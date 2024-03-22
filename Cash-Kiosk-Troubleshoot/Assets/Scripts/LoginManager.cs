using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class LoginScript : MonoBehaviour
{
    public static void OnButtonClick(Button button)
    {
        Debug.Log(button.ToString());
    }
}
