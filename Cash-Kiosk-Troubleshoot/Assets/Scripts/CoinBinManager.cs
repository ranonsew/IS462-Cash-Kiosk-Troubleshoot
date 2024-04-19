using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoinBinManager : MonoBehaviour
{
    public static bool unlocked = false; 
    public static XRGrabInteractable interactable;
    public GameObject coinPrefab;
    public Transform coinSpawnLocation;


    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        Debug.Log(interactable.name);
        UpdateGrabbableState();
        GameObject coin = Instantiate(coinPrefab, coinSpawnLocation.position, Quaternion.identity);

        //if (Random.value <= 0.1f) {
        //    GameObject coin = Instantiate(coinPrefab, coinSpawnLocation.position, Quaternion.identity);
        //}

    }

    public static void UpdateGrabbableState()
    {
        if (unlocked)
        {
            interactable.interactionLayers = LayerMask.GetMask("Default"); // Set the interaction layer(s) as needed
            Debug.Log("default");
        }
        else
        {
            interactable.interactionLayers = LayerMask.GetMask("Grab");
            Debug.Log("grab");

        }
    }
}
