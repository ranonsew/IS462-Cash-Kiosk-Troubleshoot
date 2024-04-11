using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField password;

    public GameObject wrongScreen;
    public GameObject nextPanel;

    public ScreenManager screenManager;
    public GameObject nextScreenPanel;

    public bool loginActive = true; 
    public bool passwordActive = false;

    private void Start()
    {
        login.gameObject.SetActive(loginActive);
        password.gameObject.SetActive(passwordActive);
        wrongScreen.SetActive(false);
        password.contentType = TMP_InputField.ContentType.Password;
    }

    public bool checkLoginUser(string login)
    {
        return login == "3333";
    }

    public bool checkLoginPassword(string password)
    {
        return password == "123123";
    }

    public void enterInput(string character)
    {
        if (loginActive)
        {
            login.text += character;
        } 
        else
        {
            password.text += character;
        }
    }

    public void clearInput()
    {
        if (loginActive)
        {
            login.text = string.Empty;
        }
        else
        {
            password.text = string.Empty;
        }
    }

    public void removeLastChar()
    {
        if (loginActive & login.text.Length > 0)
        {
            login.text = login.text.Substring(0, login.text.Length - 1);
        }
        else if (passwordActive & password.text.Length > 0)
        {
            password.text = password.text.Substring(0, password.text.Length - 1);
        }
    }

    public void buttonClick(TextMeshProUGUI text)
    {
        string character = text.text;
        
        if (character != "ENTER" & character != "EJECT" & character != "CLEAR" & character != "<")
        {
            enterInput(character);
        }
        else if (character == "CLEAR")
        {
            clearInput();
        }
        else if (character == "<")
        {
            removeLastChar();
        }
        else if (character == "ENTER") 
        {
            if (loginActive)
            {
                if (checkLoginUser(login.text)) {
                    loginActive = false;
                    passwordActive = true;
                    login.gameObject.SetActive(loginActive);
                    password.gameObject.SetActive(passwordActive);
                }
                else
                {
                    wrongScreen.SetActive(true);
                }
            }
            else if (checkLoginUser(login.text) & checkLoginPassword(password.text)) 
            {
                Debug.Log("Login Success");

                DateTime dt = System.DateTime.Now;

                StorageManager.dateString = "Login Date: " + dt.ToString("dd/MM/yyyy");
                StorageManager.timeString = "Login Time: " + dt.ToString("HH:mm:ss");
                StorageManager.userString = "Login ID: 3333";

                screenManager.SwitchScreen(nextScreenPanel);
            }
            else if (!checkLoginPassword(password.text) || !checkLoginUser(login.text))
            {
                wrongScreen.SetActive(true);
            }
        } 

    }

}
