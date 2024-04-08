using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDialogManager : MonoBehaviour
{
    public Canvas phoneDialogCanvas;
    public List<DialogEntry> dialogEntries = new List<DialogEntry>();

    private Dictionary<string, GameObject> dialogs = new Dictionary<string, GameObject>();
    private string currentDialog = "start";

    [System.Serializable]
    public struct DialogEntry
    {
        public string name;
        public GameObject dialogObject;
    }

    void Start()
    {
        // Ensure that the canvas is initially disabled
        phoneDialogCanvas.enabled = false;

        // Populate the dictionary from dialogEntries
        foreach (var entry in dialogEntries)
        {
            dialogs.Add(entry.name, entry.dialogObject);
            entry.dialogObject.SetActive(false);
        }
    }

    public void StartCall()
    {
        phoneDialogCanvas.enabled = true;
        currentDialog = "start";
        dialogs[currentDialog].SetActive(true);
    }

    public void EndCall()
    {
        phoneDialogCanvas.enabled = false;
        dialogs[currentDialog].SetActive(false);
    }

    public void SwitchDialog(string switchTo)
    {
        if (switchTo == "heartbeatCheck")
        {
            // check if hearbeat is successful
        }
        else
        {
            dialogs[currentDialog].SetActive(false);
            currentDialog = switchTo;
            dialogs[currentDialog].SetActive(true);
        }
    }

    public void ReturnToBase(bool isComplete)
    {
        // go to results page. if not complete, fail
    }
}
