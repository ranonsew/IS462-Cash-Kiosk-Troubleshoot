using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Kiosk Controller Script
 * controls machine state
 */

public class KioskController : MonoBehaviour
{
    private GameObject alarm; // test
    public Canvas screenCanvas; // the canvas wherein the screen is
    public GameObject currentScreenPanel; // the current screen, initial for first one

    // Start is called before the first frame update
    void Start()
    {
        // Test
        alarm = transform.Find("Alarm").gameObject;
        if (alarm == null)
        {
            Debug.LogError("Alarm gameobject not found");
        }
        else
        {
            Debug.Log("Alarm Found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // like the reset lights or something idk
    public void Lights()
    {
        // get lights game object probably
        // change lights, something something
    }

    public void ToggleLockTop()
    {
        // get the top locky bit
        // unlock or something (whenever it's done or whatever)
    }

    public void Login()
    {

    }

    public void SwitchPanel(string screenName)
    {
        currentScreenPanel.SetActive(false); // set current to false
        currentScreenPanel = screenCanvas.transform.Find(screenName).gameObject; // set a new current
        currentScreenPanel.SetActive(true); // set the new current to true
        Debug.Log($"Switched to: {currentScreenPanel.name}");
    }
}
