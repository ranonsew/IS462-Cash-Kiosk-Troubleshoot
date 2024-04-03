using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Screen Management script
/// </summary>
public class ScreenManager : MonoBehaviour
{
    public Canvas screenCanvas; // pass in via inspector
    public GameObject currentScreenPanel; // set in inspector

    // Start is called before the first frame update
    void Start()
    {
        currentScreenPanel.SetActive(true); // ensure active
        foreach (Transform child in screenCanvas.transform)
        {
            // if something in hierarchy is not currentScreenPanel when script is first loaded
            if (child.gameObject.activeInHierarchy && child.gameObject != currentScreenPanel)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public function for switching screen panels (unfortunately, needs to be attached to all the necessary buttons
    // How to attach? Go to button obj, scroll down in inspector to "On Click ()", drag in "Screen" obj, then function,
    // Then choose "SwitchScreen(GameObject nextScreenPanel)" & drag in panel GameObject
    public void SwitchScreen(GameObject nextScreenPanel)
    {
        currentScreenPanel.SetActive(false);
        currentScreenPanel = nextScreenPanel;
        currentScreenPanel.SetActive(true);
        Debug.Log($"Switched to {currentScreenPanel.name}");
    }

    //public void SwitchScreen(string screenName)
    //{
    //    Transform newPanel = screenCanvas.transform.Find(screenName);
    //    // To check ensure that using Panel
    //    //if (!newPanel.CompareTag(panelTag))
    //    //{
    //    //    throw new System.InvalidOperationException($"Need to ensure it's a {panelTag} tagged panel");
    //    //}
    //    currentScreenPanel.SetActive(false);
    //    currentScreenPanel = newPanel.gameObject;
    //    currentScreenPanel.SetActive(true);
    //    Debug.Log($"Switched to {screenName}");
    //}
}
