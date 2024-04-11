using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatFailureManager : MonoBehaviour
{
    private void Start()
    {
        StorageManager.errorTextActive = true;
        StorageManager.errorType = "OMG IS IT WORKING";
    }
    private IEnumerator RebootMachine(LightPulseManager lightPulseManager, ChangeLightColour changeLightColour)
    {
        lightPulseManager.StartPulsating();

        // wait for 10 to 15s, then stop and change to yellow
        float waitTime = Random.Range(10f, 15f);
        Debug.Log(waitTime);
        yield return new WaitForSeconds(waitTime);

        lightPulseManager.StopPulsating();
        changeLightColour.blueToYellow(true, true);
    }
}
