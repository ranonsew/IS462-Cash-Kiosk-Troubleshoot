using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
//using System.Diagnostics;

public class MainPanelManager : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;

    public ChangeLightColour changeLightColour;
    public LightPulseManager lightPulseManager;

    public ScreenManager screenManager;
    public GameObject nextScreenPanel;
    public GameObject inventoryScreenPanel;
    public GameObject loginPanel;

    public GameObject rebootPanel; 
    public bool rebootActive = false;

    public GameObject mainPanel1;
    public bool mainPanel1Active = true;

    public GameObject mainPanel2;
    public bool mainPanel2Active = false;

    public TextMeshProUGUI errorText;
    public GameObject loading;

    public ReceiptManager receiptScript;

    public InstructionManager i;

    void Start()
    {

        // receiptScript = GetComponent<ReceiptManager>();
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

        
        // start of pre-scenario stuff
        UnityEngine.Debug.Log("Clicked text! view inventory?" + text.text);
        if (text.text == "View Inventory"){
            UnityEngine.Debug.Log("Clicked text! view inventory?" + text.text);
            screenManager.SwitchScreen(inventoryScreenPanel);
            return;
        }

        if (text.text == "Print"){
            UnityEngine.Debug.Log("Clicked text! print?" + text.text);
            receiptScript = GetComponent<ReceiptManager>();
            receiptScript.InventoryReceipt();
            return;
        }

        if (text.text == "Collect Notes"){
            UnityEngine.Debug.Log("Clicked text! Collect Notes?" + text.text);
            receiptScript = GetComponent<ReceiptManager>();
            receiptScript.collectionReceipt("Notes");
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

            loading.SetActive(true);

            changeLightColour.yellowToBlue(true, true);

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

        loading.SetActive(false);

        lightPulseManager.StopPulsating();
        changeLightColour.blueToYellow(true, true);

        HeartbeatCheck.heartbeatFailure = false;

        StorageManager.errorType = "Machine connected";
        errorText.text = StorageManager.errorType;
        errorText.color = Color.green;
        
        if (SceneManager.GetActiveScene().name == "SceneE")
        {
            i.LoadNextInstructions();
        }
    }
}
