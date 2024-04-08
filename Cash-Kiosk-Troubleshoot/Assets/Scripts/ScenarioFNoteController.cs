using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

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

    public GameObject drawerLock;

    /// <summary>
    /// Couldn't get regular note prefab to work, but can with RejectedNote prefab
    /// </summary>
    public RejectedNote noteStuck;

    // as of current machine/note positions
    // -0.2389 | inspector
    // -3.183544 | code
    // -2.944644 | difference
    

    private readonly float notePosMinX = -0.671f;
    private readonly float notePosMaxX = 0.671f;
    private float maxRangeX;

    private readonly float notePosMinY = 0.521f;
    private readonly float notePosMaxY = 0.983f;
    private float maxRangeY;

    private readonly float notePosMinZ = -0.49f;
    private readonly float notePosMaxZ = 0.49f;
    private float maxRangeZ;

    // Start is called before the first frame update
    void Start()
    {
        dialKnob.onValueChange.AddListener(UpdateNotePosition);
        maxRangeX = notePosMaxX - notePosMinX;
        maxRangeY = notePosMaxY - notePosMinY;
        maxRangeZ = notePosMaxZ - notePosMinZ;
        Debug.Log($"Max Range: {maxRangeX}, {maxRangeY}, {maxRangeZ}");


        // set randomized note position & default knob position
        noteStuck.transform.localPosition = new Vector3(Random.Range(notePosMinX, notePosMaxX), 0.535f, 0f); // randomized X position
        Debug.Log($"Value: {dialKnob.value}");
        Debug.Log($"NoteX: {noteStuck.transform.position.x}");
    }

    // Update is called once per frame
    void Update()
    {
        // using Update() to check on the local position of the note, if within bounds of the machine
        // note must be within bounds of -0.671 <= x <= 0.671, -0.49 <= z <= 0.49, 0.521 <= y <= 0.983 
    }

    /// <summary>
    /// Update the knob rotation based on location of the note
    /// </summary>
    private void UpdateKnobRotation()
    {
        float x = noteStuck.transform.localPosition.x;
        float y = noteStuck.transform.localPosition.y;
        float z = noteStuck.transform.localPosition.z;

        bool withinX = x >= notePosMinX && x <= notePosMaxX;
        bool withinY = y >= notePosMinY && y <= notePosMaxY;
        bool withinZ = z >= notePosMinZ && z <= notePosMaxZ;

        // if within X, Y, Z boundaries
        if (withinX && withinY && withinZ)
        {
            dialKnob.value = (noteStuck.transform.localPosition.x - notePosMinX) / maxRangeX; // update to match x position
        }
        
    }

    /// <summary>
    /// Updates note position
    /// </summary>
    /// <param name="val">changed knob value</param>
    private void UpdateNotePosition(float val)
    {
        Debug.Log($"Knob value: {val}");
        float newX = (maxRangeX * val) + notePosMinX;
        Debug.Log($"Note new X pos: {newX}");

        noteStuck.transform.localPosition = new Vector3(newX, noteStuck.transform.localPosition.y, noteStuck.transform.localPosition.z);
    }

    /// <summary>
    /// Unlock Drawer Function (for screen to use) -- attach ScenarioFController obj to the screen button that unlocks the drawer
    /// </summary>
    public void UnlockDrawer()
    {
        drawerLock.SetActive(false);
    }

    /// <summary>
    /// Lock Drawer Function (for RecyclerCover to use)
    /// </summary>
    public void LockDrawer()
    {
        drawerLock.SetActive(true);
    }
}
