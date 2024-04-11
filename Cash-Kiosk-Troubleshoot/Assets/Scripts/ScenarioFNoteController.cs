using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Class to control the 
/// </summary>
public class ScenarioFNoteController : MonoBehaviour
{
    /// <summary>
    /// Knob dial thing
    /// </summary>
    [Tooltip("LowerDialKnob thingy ")]
    public XRKnob dialKnob;

    /// <summary>
    /// For locking/unlocking of drawer mechanic
    /// </summary>
    public GameObject drawerLock;
    public GameObject noteRecycler001;
    [HideInInspector] public bool allowLock = false; // disallow locking by default (see LockDrawer() for more info)
    public GameObject fillerCube;

    public ChangeLightColour changeLightColor;
    public LightPulseManager lightPulseManager;

    public InstructionManager instructionManager;
    public TextMeshProUGUI screen2ErrorText;

    /// <summary>
    /// Couldn't get regular note prefab to work, but can with RejectedNote prefab
    /// </summary>
    public RejectedNote noteStuck;

    private static readonly float notePosMinX = -0.671f;
    private static readonly float notePosMaxX = 0.671f;
    private float maxRangeX = notePosMaxX - notePosMinX;

    private static readonly float notePosMinY = 0.521f;
    private static readonly float notePosMaxY = 0.983f;
    private float maxRangeY = notePosMaxY - notePosMinY;

    private static readonly float notePosMinZ = -0.49f;
    private static readonly float notePosMaxZ = 0.49f;
    private float maxRangeZ = notePosMaxZ - notePosMinZ;

    // Start is called before the first frame update
    void Start()
    {
        dialKnob.onValueChange.AddListener(UpdateNotePosition);

        changeLightColor.blueToYellow(true, true); // change to yellow default

        // set randomized note position & default knob position
        float x = Random.Range(notePosMinX, notePosMaxX);
        float y = 0.535f;
        float z = 0f;
        noteStuck.transform.localPosition = new Vector3(x, y, z); // randomized X position
        UpdateKnobRotation();
        noteStuck.GetComponent<XRGrabInteractable>().selectExited.AddListener(UpdateKnobWrapper);
    }

    /// <summary>
    /// Note within the bounds of the recycler internal drawer track
    /// </summary>
    /// <returns></returns>
    public bool CheckNotePosition()
    {
        float x = noteStuck.transform.localPosition.x;
        float y = noteStuck.transform.localPosition.y;
        float z = noteStuck.transform.localPosition.z;

        bool withinX = x >= notePosMinX & x <= notePosMaxX;
        bool withinY = y >= notePosMinY & y <= notePosMaxY;
        bool withinZ = z >= notePosMinZ & z <= notePosMaxZ;

        return withinX & withinY & withinZ;
    }

    void UpdateKnobWrapper(SelectExitEventArgs args)
    {
        UpdateKnobRotation();
    }

    /// <summary>
    /// Update the knob rotation based on location of the note
    /// </summary>
    private void UpdateKnobRotation()
    {
        // if within X, Y, Z boundaries
        if (CheckNotePosition())
        {
            float val = (noteStuck.transform.localPosition.x - notePosMinX) / maxRangeX;
            Debug.Log($"Updated Knob value: {val}");
            dialKnob.value = val; // update to match x position
        }
    }

    /// <summary>
    /// Updates note position via knob on change
    /// </summary>
    /// <param name="val">changed knob value</param>
    private void UpdateNotePosition(float val)
    {
        if (CheckNotePosition())
        {
            float newX = (maxRangeX * val) + notePosMinX;
            noteStuck.transform.localPosition = new Vector3(newX, noteStuck.transform.localPosition.y, noteStuck.transform.localPosition.z);
        }
    }

    /// <summary>
    /// Unlock Drawer Function (for screen to use) -- attach ScenarioFController obj to the screen button that unlocks the drawer
    /// </summary>
    public void UnlockDrawer()
    {
        drawerLock.GetComponent<BoxCollider>().isTrigger = true;
        allowLock = false;
        screen2ErrorText.text = "Upper Unlocked";

        // Add instruction manager step here
    }

    /// <summary>
    /// Lock Drawer Function (for RecyclerCover to use)
    /// </summary>
    public void LockDrawer()
    {
        if (allowLock)
        {
            drawerLock.GetComponent<BoxCollider>().isTrigger = false;
            allowLock = false; // prep for next lock/unlock cycle if there is
            StartCoroutine(FlashingLightsRebootEmulator());
            Debug.Log("Lock allowed, proceeding...");

            // Add instruction manager step here
        }
    }

    IEnumerator FlashingLightsRebootEmulator()
    {
        changeLightColor.yellowToBlue(true, true);

        lightPulseManager.StartPulsating();

        yield return new WaitForSeconds(3f);

        lightPulseManager.StopPulsating();

        changeLightColor.blueToYellow(true, true);

        // Add instruction manager step here
    }
}
