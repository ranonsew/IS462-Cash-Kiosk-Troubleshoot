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

    /// <summary>
    /// Couldn't get regular note prefab to work, but can with RejectedNote prefab
    /// </summary>
    public RejectedNote noteStuck;

    // -0.2389 | inspector
    // -3.183544 | code
    private float notePosMin = -0.671f;
    private float notePosMax = 0.671f;

    // Start is called before the first frame update
    void Start()
    {
        dialKnob.onValueChange.AddListener(UpdateNotePosition);
        Debug.Log($"Value: {dialKnob.value}");
        Debug.Log($"NoteX: {noteStuck.transform.position.x}");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Updates note position
    /// </summary>
    /// <param name="val">changed knob value</param>
    private void UpdateNotePosition(float val)
    {
        Debug.Log($"Knob value: {val}");
        float newX = noteStuck.transform.position.x + val;
        Debug.Log($"Note new X pos: {newX}");

        // emulate end of track, preventing from getting too stuck
        // min: -0.671
        // max: 0.671

        if (newX >= notePosMin && newX <= notePosMax)
        {
            noteStuck.transform.position = new Vector3(newX, noteStuck.transform.position.y, noteStuck.transform.position.z);
        }

        if (newX < notePosMin || newX > notePosMax)
        {
            Debug.Log("Note going out of bounds, stop!!");
        }


    }
}
