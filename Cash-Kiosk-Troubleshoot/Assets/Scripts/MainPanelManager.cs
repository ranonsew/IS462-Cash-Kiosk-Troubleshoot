using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class MainPanelManager : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;

    public ChangeLightColour changeLightColour;
    public LightPulseManager lightPulseManager;

    public ScreenManager screenManager;
    public GameObject nextScreenPanel;
    public GameObject loginPanel;

    public GameObject rebootPanel; 
    public bool rebootActive = false;

    public GameObject mainPanel1;
    public bool mainPanel1Active = true;

    public GameObject mainPanel2;
    public bool mainPanel2Active = false;

    public TextMeshProUGUI errorText;
    
    void Start()
    {
        GameObject attachedGameObject = gameObject;
        bool errorActive = StorageManager.errorTextActive;

        if (attachedGameObject == mainPanel2)
        {
            mainPanel1Active = false;
            mainPanel2Active = true;
        }

        if (errorActive)
        {
            errorText.text = StorageManager.errorType;
        }

        rebootPanel.SetActive(rebootActive);
        mainPanel1.SetActive(mainPanel1Active);
        mainPanel2.SetActive(mainPanel2Active);
        errorText.gameObject.SetActive(errorActive);

        dateText.text = StorageManager.dateString;
        timeText.text = StorageManager.timeString;
    }

    public void buttonClick(TextMeshProUGUI text)
    {

        Debug.Log("Clicked text! view inventory?" + text.text);

        if (text.text == "View Inventory"){
            

            return;
        }


        if (checkNextPage(text.text))
        {
            screenManager.SwitchScreen(nextScreenPanel);
        }
        else if (checkReboot(text.text)) 
        {
            rebootActive = true;
            rebootPanel.SetActive(rebootActive);
        } 
        else if (checkLogout(text.text))
        {
            screenManager.SwitchScreen(loginPanel);
        }
        else if (rebootActive & text.text == "Yes")
        {
            UnityEngine.Debug.Log("cycling machine time");
            rebootActive = false;
            rebootPanel.SetActive(rebootActive);

            // reboot machine
            StartCoroutine(RebootMachine());
        }
    }

    public bool checkNextPage(string str)
    {
        return str == "More Functions";
    }

    public bool checkReboot(string str)
    {
        return str == "Reboot PC";
    }

    public bool checkLogout(string str)
    {
        return str == "Logout";
    }

    private IEnumerator RebootMachine()
    {
        // make the light pulse
        lightPulseManager.StartPulsating();

        // wait for 10 to 15s, then stop and change to yellow
        float waitTime = UnityEngine.Random.Range(10f, 15f);
        UnityEngine.Debug.Log(waitTime);
        yield return new WaitForSeconds(waitTime);

        lightPulseManager.StopPulsating();
        changeLightColour.blueToYellow(true, true);

        HeartbeatCheck.heartbeatFailure = false;
    }
}
