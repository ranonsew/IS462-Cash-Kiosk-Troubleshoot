using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using TMPro;
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
    public TextMeshProUGUI screen2ErrorText;
    
    public InstructionManager instructionManager;
    public SceneLoader sceneLoader;
    private bool grabChecker = true;
    private bool completionCheck = false;

    /// <summary>
    /// Couldn't get regular note prefab to work, but can with RejectedNote prefab
    /// </summary>
    public RejectedNote noteStuck;

    private bool noteInSeal = false;
    private bool machineRebooted = false;

    private static readonly float notePosMinX = -0.434f;
    private static readonly float notePosMaxX = 0.671f;
    private float maxRangeX = notePosMaxX - notePosMinX;

    private static readonly float notePosMinY = 0.521f;
    private static readonly float notePosMaxY = 0.983f;

    private static readonly float notePosMinZ = -0.49f;
    private static readonly float notePosMaxZ = 0.49f;

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
        noteStuck.GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnNoteGrabbedInstructionWrapper); // add instruction manager wrapper bit here
        noteStuck.GetComponent<XRGrabInteractable>().selectExited.AddListener(OnNoteReleasedInstructionWrapper);

        StartCoroutine(DelayedStartingInstructions()); // second instruction
    }

    private IEnumerator DelayedStartingInstructions()
    {
        yield return new WaitForSeconds(10f);

        instructionManager.LoadSpecificInstructionIndex(1); // To start, login with ...
    }

    private void WaitLoadResultsScene() {
        StartCoroutine(LoadResultsScene());
    }

    private IEnumerator LoadResultsScene(){
        yield return new WaitForSeconds(6);
        sceneLoader.LoadScene("SceneResults");
    }

    void Update()
    {
        if (machineRebooted & noteInSeal & !completionCheck)
        {
            completionCheck = true;
            PointsManager.instance.updateScore("SceneF", "completionRate", 1f);
            PointsManager.instance.updateScore("SceneF", "numErrors", 0f);
            WaitLoadResultsScene();

            // StartCoroutine(DelayToEnd());
        }
    }

    /// <summary>
    /// Wrapper function for LoadSpecificInstruction at index 4
    /// </summary>
    /// <param name="args"></param>
    private void OnNoteGrabbedInstructionWrapper(SelectEnterEventArgs args)
    {
        if (CheckNotePosition()) // if in machine
        {
            instructionManager.LoadSpecificInstructionIndex(4); // Make sure to put the note ...
        }
        else if (grabChecker) // if outside machine
        {
            instructionManager.LoadSpecificInstructionIndex(6); // With this note grabbed, ...
            grabChecker = false;
        }
    }

    private void OnNoteReleasedInstructionWrapper(SelectExitEventArgs args)
    {
        StartCoroutine(delaytocheck());

        IEnumerator delaytocheck()
        {
            yield return new WaitForSeconds(3f);
            grabChecker = true;
        }
    }

    /// <summary>
    /// Note within the bounds of the recycler internal drawer track
    /// </summary>
    /// <returns></returns>
    public bool CheckNotePosition()
    {
        if (!noteStuck)
        {
            return false;
        }

        float x = noteStuck.transform.localPosition.x;
        float y = noteStuck.transform.localPosition.y;
        float z = noteStuck.transform.localPosition.z;

        bool withinX = x >= notePosMinX & x <= notePosMaxX;
        bool withinY = y >= notePosMinY & y <= notePosMaxY;
        bool withinZ = z >= notePosMinZ & z <= notePosMaxZ;

        return withinX & withinY & withinZ;
    }

    /// <summary>
    /// Wrapper function for UpdateKnobRotation()
    /// </summary>
    /// <param name="args"></param>
    private void UpdateKnobWrapper(SelectExitEventArgs args)
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

        instructionManager.LoadSpecificInstructionIndex(2); // Nice work! The upper part ...
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
        }
    }

    /// <summary>
    /// Emulate the machine rebooting
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashingLightsRebootEmulator()
    {
        changeLightColor.yellowToBlue(true, true);

        lightPulseManager.StartPulsating();

        yield return new WaitForSeconds(3f);

        lightPulseManager.StopPulsating();

        changeLightColor.blueToYellow(true, true);

        yield return new WaitForSeconds(0.5f);

        // as long as it is outside the position
        if (!CheckNotePosition())
        {
            instructionManager.LoadSpecificInstructionIndex(5); // Congratulations! The yellow lights returning ...
            machineRebooted = true;
        }
        else
        {
            instructionManager.LoadSpecificInstructionIndex(7); // Uh oh, it looks like the machine ...
        }
    }

    /// <summary>
    /// Select enter event on the rejected note
    /// </summary>
    /// <param name="args"></param>
    public void ScenarioF_EndSceneLoader(SelectEnterEventArgs args)
    {
        noteInSeal = true;
    }
}
