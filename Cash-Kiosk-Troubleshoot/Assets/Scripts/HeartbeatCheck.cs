using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartbeatCheck : MonoBehaviour
{
    public static bool heartbeatFailure = false;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "SceneE")
        {
            heartbeatFailure = true;
            StorageManager.errorTextActive = true;
            StorageManager.errorType = "Machine not connected";
        }
    }
}
