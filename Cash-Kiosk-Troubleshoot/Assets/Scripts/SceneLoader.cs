using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private List<string> scenarioScenes = new List<string>()
    {
        "CollectionAndReplenishment",
        "SceneC",
        "SceneD",
        "SceneE",
        "SceneF",
    };


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadRandomScene()
    {
        int rand = Random.Range(0, scenarioScenes.Count);
        LoadScene(scenarioScenes[rand]);
    }
}
