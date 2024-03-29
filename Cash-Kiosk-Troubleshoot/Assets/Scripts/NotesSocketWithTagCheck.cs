using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NotesSocketWithTagCheck : XRSocketInteractor
{
    public string[] targetTag;
    public GameObject stackOfNotesPrefab;
    public float noteMoveSpeed = 10f;
    public float noteDelay = 0.1f;

    private Vector3 originalPosition;
    private GameObject[] notes;

    public override bool CanHover(IXRHoverInteractable interactable) {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(IXRSelectInteractable interactable) {
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    private bool MatchUsingTag(IXRInteractable interactable) {
        XRBaseInteractable XRBaseInteractable = interactable as XRBaseInteractable;
        if (XRBaseInteractable == null) return false;

        bool tagMatch = false;
        foreach (string tag in targetTag)
        {
            tagMatch = tagMatch || XRBaseInteractable.CompareTag(tag);
        }
        return tagMatch;
    }

    public void DestroyPlacedObject(SelectEnterEventArgs args) {
        XRBaseInteractable XRBaseInteractable = args.interactableObject as XRBaseInteractable;
        originalPosition = XRBaseInteractable.gameObject.transform.position;
        Destroy(XRBaseInteractable.gameObject);
    }

    public void CreateObject(SelectEnterEventArgs args)
    {
        GameObject stackOfNotes = Instantiate(stackOfNotesPrefab, originalPosition, Quaternion.identity);

        // Remove XR Grab Interactable component from the instantiated object
        XRGrabInteractable grabInteractable = stackOfNotes.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            Destroy(grabInteractable);
        }

        // disable physics on the instantiated object if it's not needed
        Rigidbody rb = stackOfNotes.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Get all the notes in the stack
        notes = new GameObject[stackOfNotes.transform.childCount];
        for (int i = 0; i < stackOfNotes.transform.childCount; i++)
        {
            notes[i] = stackOfNotes.transform.GetChild(i).gameObject;
        }

        // Start moving notes
        StartCoroutine(MoveNotes());
    }

    IEnumerator MoveNotes()
    {
        // Iterate through each note in the stack
        for (int i = notes.Length - 1; i >= 0; i--)
        {
            // Calculate the destination x position
            float destinationX = notes[i].transform.position.x - 0.5f; // Adjust the offset as needed

            // Move the note towards the destination x position
            while (notes[i].transform.position.x > destinationX)
            {
                notes[i].transform.Translate(Vector3.left * noteMoveSpeed * Time.deltaTime);
                yield return null;
            }

            // Delay before moving the next note
            yield return new WaitForSeconds(noteDelay);
        }
    }
}
