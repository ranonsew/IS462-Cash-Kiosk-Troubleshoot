using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoinBinManager : MonoBehaviour
{
    public static bool unlocked = false; 
    public static XRGrabInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        UpdateGrabbableState();
    }

    public static void UpdateGrabbableState()
    {
        if (unlocked)
        {
            interactable.interactionLayers = LayerMask.GetMask("Default"); // Set the interaction layer(s) as needed
        }
        else
        {
            interactable.interactionLayers = 0;
        }
    }
}
