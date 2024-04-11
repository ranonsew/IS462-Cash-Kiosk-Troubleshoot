using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartbeatCheck : MonoBehaviour
{
    public static bool heartbeatFailure = false;
    public static bool loginDone = false;
    public InstructionManager instructionManager;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "SceneE")
        {
            heartbeatFailure = true;
            StorageManager.errorTextActive = true;
            StorageManager.errorType = "Machine not connected";

            StartCoroutine(nextInstructionWithTimer(6f));
            StartCoroutine(nextInstructionWithTimer(15f));
        }
    }

    public IEnumerator nextInstructionWithTimer(float number)
    {
        yield return new WaitForSeconds(number);
        instructionManager.LoadNextInstructions();
    }

}
