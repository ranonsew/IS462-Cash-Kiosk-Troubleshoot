using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class LoginScript : MonoBehaviour
{
    public TextMeshProUGUI loginInput;
    public TextMeshProUGUI passwordInput;

    public static void OnButtonClick(Button button)
    {
        Debug.Log(button.ToString());
    }

    // on click "enter" login() function
    public void Login()
    {
        Debug.Log("Enter pressed");
    }

    // on click "clear" function -- clears inputs
    public void ClearInput()
    {
        Debug.Log("Clearing...");
        loginInput.text = "";
        passwordInput.text = "";
    }
}
