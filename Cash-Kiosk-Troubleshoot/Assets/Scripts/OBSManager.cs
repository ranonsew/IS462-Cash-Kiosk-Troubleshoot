using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

/*
 * OBS Manager -- managing connection to api that opens OBS (because there's not easy way to connect to OBS directly in C# (afaik))
 */

// Delegate Response Callback function
public delegate void RespCb(string response);

[System.Serializable]
public class Scene
{
    public int sceneIndex;
    public string sceneName;
}

[System.Serializable]
public class SceneList
{
    public Scene[] scenes;
}

[System.Serializable]
public class CreateSceneReq
{
    public string sceneName;
}

//[System.Serializable]
//public class CreateSceneResp
//{
//    public string sceneUuid;
//}

//[System.Serializable]
//public class MsgResp
//{
//    public string message;
//}

//[System.Serializable]
//public class CreateInputResp
//{
//    public string inputUuid;
//    public int sceneItemId;
//}

// not including stuff we don't need, JsonUtility removes the unnecessary stuff (i think)
[System.Serializable]
public class SceneItem
{
    public string inputKind;
    public bool sceneItemEnabled;
    public string sourceName;
    public int sceneItemId;
}

[System.Serializable]
public class SceneItemList
{
    public SceneItem[] sceneItems;
}

[System.Serializable]
public class SceneItemEnabled
{
    public string sceneName;
    public bool sceneItemEnabled;
    public int sceneItemId;
}

[System.Serializable]
public class CreateInputReq
{
    public string sceneName;
    public string inputKind;
    public string inputName;
    public bool sceneItemEnabled;
}

[System.Serializable]
public class InputSettings
{
    public string monitor_id;
}

[System.Serializable]
public class InputSettingsResp
{
    public string inputKind;
    public InputSettings inputSettings;
}

[System.Serializable]
public class PropertyItems
{
    public string itemName;
    public bool itemEnabled;
    public string itemValue;
}

[System.Serializable]
public class PropertyItemsList
{
    public PropertyItems[] propertyItems;
}

[System.Serializable]
public class InputSettingsReq
{
    public string inputName;
    public InputSettings inputSettings;
}


public class OBSManager : MonoBehaviour
{
    // test data
    private readonly string baseURL = "http://localhost:45713";
    private readonly string sceneName = "unity_test1";
    private readonly string inputKind = "monitor_capture"; // one of the preset input kinds that OBS allows
    private readonly string inputName = "unity_inp1"; // -- as part of example, we're tying the name to the specific input_kind

    // Start is called before the first frame update
    void Start()
    {
        ConnObsDispCapt();
    }

    private async void ConnObsDispCapt()
    {
        // Step 1. Check for scene, hasScene is true if it's there
        string resp = await AsyncGetReq("/scene/all");
        SceneList sceneList = JsonUtility.FromJson<SceneList>(resp);
        bool hasScene = false;
        foreach (Scene scene in sceneList.scenes)
        {
            if (scene.sceneName == sceneName)
            {
                hasScene = true;
                break;
            }
        }

        if (!hasScene)
        {
            Debug.Log("Scene not found, going to create");
            // Step 1a. Create new scene
            CreateSceneReq csreq = new CreateSceneReq()
            {
                sceneName = sceneName,
            };
            resp = await AsyncPostReq("/scene/create", JsonUtility.ToJson(csreq));
            //CreateSceneResp csresp = JsonUtility.FromJson<CreateSceneResp>(resp);
        }


        // Step 2. Switch to correct scene
        resp = await AsyncGetReq($"/scene/change/{sceneName}");
        //MsgResp msgresp = JsonUtility.FromJson<MsgResp>(resp);


        // Step 3. Check for input (source)
        resp = await AsyncGetReq($"/sceneItems/{sceneName}");
        SceneItemList sil = JsonUtility.FromJson<SceneItemList>(resp);
        bool inputNameExists = false;
        bool inputKindExists = false;
        bool sceneItemEnabled = false;
        int sceneItemId = 0; // to prevent red squiggly
  
        if (sil.sceneItems != null) // foreach if not null
        {
            foreach (SceneItem item in sil.sceneItems)
            {
                if (item.sourceName == inputName)
                {
                    inputNameExists = true;
                    if (item.inputKind == inputKind)
                    {
                        inputKindExists = true;
                        sceneItemEnabled = item.sceneItemEnabled; // setting enabled state to be same
                        sceneItemId = item.sceneItemId; // providing the sceneItemId
                        break;
                    }
                    break; // break if sourceName is inputName, but not correct inputKind
                }
            }
        }

        // Check input kind existing (means also inputName exists)
        if (inputKindExists)
        {
            // Check if the sceneitem is not enabled, then need to enable
            if (!sceneItemEnabled)
            {
                // Step 3a. Enable the scene item (input)
                SceneItemEnabled siereq = new SceneItemEnabled()
                {
                    sceneName = sceneName,
                    sceneItemEnabled = true,
                    sceneItemId = sceneItemId,
                };
                resp = await AsyncPostReq("/sceneItems/setEnabled", JsonUtility.ToJson(siereq));
                //MsgResp msgresp = JsonUtility.FromJson<MsgResp>(resp);
            }
            // if all good, skip over to Step 4.
        }
        // Else -- if input name exists without input kind, need to remove input
        else
        {
            // Step 3a(i). Delete the existing input
            if (inputNameExists)
            {
                resp = await AsyncDelReq($"/input/delete/{inputName}");
                //MsgResp msgresp = JsonUtility.FromJson<MsgResp>(resp);
            }

            // Step 3b. Add the correct input kind
            CreateInputReq cireq = new CreateInputReq()
            {
                sceneName = sceneName,
                sceneItemEnabled = true,
                inputKind = inputKind,
                inputName = inputName,
            };
            resp = await AsyncPostReq("/input/create", JsonUtility.ToJson(cireq));
            //CreateInputResp ciresp = JsonUtility.FromJson<CreateInputResp>(resp);
        }


        // Step 4. Get Input Properties -- monitor_id because we using monitor_capture inputKind
        resp = await AsyncGetReq($"/input/{inputName}/properties/monitor_id");
        PropertyItemsList pil = JsonUtility.FromJson<PropertyItemsList>(resp);
        string monitor_id = pil.propertyItems[0].itemValue;


        // Step 5. Set the monitor_id into the input settings
        InputSettings inputSettings = new InputSettings()
        {
            monitor_id = monitor_id,
        };
        InputSettingsReq isreq = new InputSettingsReq()
        {
            inputName = inputName,
            inputSettings = inputSettings,
        };
        resp = await AsyncPostReq($"/input/settings", JsonUtility.ToJson(isreq));
        //MsgResp msgresp = JsonUtility.FromJson<MsgResp>(resp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Async Get Request Function
    private async Task<string> AsyncGetReq(string path)
    {
        using (UnityWebRequest r = UnityWebRequest.Get(baseURL + path))
        {
            r.SendWebRequest();
            while (!r.isDone)
            {
                await Task.Yield();
            }

            if (r.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {r.downloadHandler.text}");
                return r.error;
            }
            else
            {
                Debug.Log($"Response: {r.downloadHandler.text}");
                return r.downloadHandler.text;
            }
        }
    }

    // Async Post Request Function
    private async Task<string> AsyncPostReq(string path, string data)
    {
        using (UnityWebRequest r = UnityWebRequest.Post(baseURL + path, data, "application/json"))
        {
            r.SendWebRequest();
            while (!r.isDone)
            {
                await Task.Yield();
            }

            if (r.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {r.downloadHandler.text}");
                return r.error;
            }
            else
            {
                Debug.Log($"Response: {r.downloadHandler.text}");
                return r.downloadHandler.text;
            }
        }
    }

    // Async Delete Request Function
    private async Task<string> AsyncDelReq(string path)
    {
        using (UnityWebRequest r = UnityWebRequest.Delete(baseURL + path))
        {
            r.SendWebRequest();
            while (!r.isDone)
            {
                await Task.Yield();
            }

            if (r.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {r.downloadHandler.text}");
                return r.error;
            }
            else
            {
                Debug.Log($"Response: {r.downloadHandler.text}");
                return r.downloadHandler.text;
            }
        }
    }
}
