using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainPanelManager : MonoBehaviour
{
    DateTime dt = System.DateTime.Now;

    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;

    //public RebootMachine rebootManager;

    public ScreenManager screenManager;
    public GameObject nextScreenPanel;

    public GameObject rebootPanel; 
    public bool rebootActive = false;

    public GameObject mainPanel1;
    public bool mainPanel1Active = true;

    public GameObject mainPanel2;
    public bool mainPanel2Active = false;

    void Start()
    {
        // get which main screen it is on
        GameObject attachedGameObject = gameObject;

        if (attachedGameObject == mainPanel2)
        {
            mainPanel1Active = false;
            mainPanel2Active = true;
        }

        rebootPanel.SetActive(rebootActive);
        mainPanel1.SetActive(mainPanel1Active);
        mainPanel2.SetActive(mainPanel2Active);

        string date = dt.ToString("dd/MM/yyyy");
        dateText.text = "Login Date: " + date;


        string time = dt.ToString("HH:mm:ss");
        timeText.text = "Login Time: " + time;
    }

    public void buttonClick(TextMeshProUGUI text)
    {
        if (checkNextPage(text.text))
        {
            screenManager.SwitchScreen(nextScreenPanel);
        }
        else if (checkReboot(text.text)) 
        {
            rebootActive = true;
            rebootPanel.SetActive(rebootActive);
        } 
        else if (rebootActive & text.text == "Yes")
        {
            Debug.Log("cycling machine time");
            //rebootManager.rebooting();
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


}
