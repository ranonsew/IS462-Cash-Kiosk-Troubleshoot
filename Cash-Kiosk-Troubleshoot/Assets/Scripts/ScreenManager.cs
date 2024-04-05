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

    void Start()
    {
        currentScreenPanel.SetActive(true);
        foreach (Transform child in screenCanvas.transform)
        {
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

    public void SwitchScreen(GameObject nextScreenPanel)
    {
        currentScreenPanel.SetActive(false);
        currentScreenPanel = nextScreenPanel;
        currentScreenPanel.SetActive(true);
        Debug.Log($"Switched to {currentScreenPanel.name}");
    }
}
