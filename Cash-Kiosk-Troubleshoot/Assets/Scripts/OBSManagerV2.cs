using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OBSManagerV2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ConnectToObsDisplayCapture("http://localhost:45713/connect-display-capture"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ConnectToObsDisplayCapture(string url)
    {
        using UnityWebRequest r = UnityWebRequest.Get(url);
        yield return r.SendWebRequest();

        if (r.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {r.error}");
        }

        Debug.Log($"Response: {r.downloadHandler.text}");
    }
}
