using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NotesSocketWithTagCheck : XRSocketInteractor
{
    public string[] targetTag;
    public GameObject stackOfNotesPrefab;
    public GameObject rejectedNotePrefab;
    public Transform rejectionPosition;
    public float noteMoveSpeed = 10f;
    public float noteDelay = 0.1f;

    private Vector3 originalPosition;
    private GameObject[] notes;
    private string objectTag;
    private int maxAccept = 0;
    private int maxReject = 2;


    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    private bool MatchUsingTag(IXRInteractable interactable)
    {
        XRBaseInteractable XRBaseInteractable = interactable as XRBaseInteractable;
        if (XRBaseInteractable == null) return false;

        bool tagMatch = false;
        foreach (string tag in targetTag)
        {
            tagMatch = tagMatch || XRBaseInteractable.CompareTag(tag);
        }
        return tagMatch;
    }

    public void DestroyPlacedObject(SelectEnterEventArgs args)
    {
        // Start the coroutine for delaying
        StartCoroutine(DelayAndDestroy(args.interactableObject as XRBaseInteractable));
    }

    private IEnumerator DelayAndDestroy(XRBaseInteractable objToDestroyXRBaseInteractable)
    {
        // update score


        yield return new WaitForSeconds(0.2f);

        // Now continue with the destruction process
        GameObject objToDestroy = objToDestroyXRBaseInteractable.gameObject;
        originalPosition = objToDestroy.transform.position;
        objectTag = objToDestroy.transform.tag;
        Destroy(objToDestroyXRBaseInteractable.gameObject);
    }

    public void CreateObject(SelectEnterEventArgs args)
    {
        // Start the coroutine for delaying
        StartCoroutine(DelayAndCreate(args));
    }

    public IEnumerator DelayAndCreate(SelectEnterEventArgs args)
    {
        yield return new WaitForSeconds(0.25f);
        if (objectTag == "Notes")
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
            StartCoroutine(MoveStackOfNotes(stackOfNotes));
        }
        else if (objectTag == "RejectedNote")
        {
            GameObject rejectedNote = Instantiate(rejectedNotePrefab, originalPosition, Quaternion.identity);

            // Remove XR Grab Interactable component from the instantiated object
            XRGrabInteractable grabInteractable = rejectedNote.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                Destroy(grabInteractable);
            }

            // disable physics on the instantiated object if it's not needed
            Rigidbody rb = rejectedNote.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Get all the notes in the stack
            notes = new GameObject[rejectedNote.transform.childCount];
            for (int i = 0; i < rejectedNote.transform.childCount; i++)
            {
                notes[i] = rejectedNote.transform.GetChild(i).gameObject;
            }

            // Start moving notes
            StartCoroutine(MoveRejectedNote(rejectedNote));
        }
    }

    IEnumerator MoveStackOfNotes(GameObject stackOfNotes)
    {
        int numRejected = 0;

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

            if (numRejected < maxReject)
            {
                numRejected += RejectNote(0.1f);
            }

            // Delay before moving the next note
            yield return new WaitForSeconds(noteDelay);
        }

        while (numRejected < maxReject)
        {
            numRejected += RejectNote(1f);
        }

        Destroy(stackOfNotes);
    }

    IEnumerator MoveRejectedNote(GameObject rejectedNote)
    {
        // Calculate the destination x position
        float destinationX = rejectedNote.transform.position.x - 0.5f; // Adjust the offset as needed

        // Move the note towards the destination x position
        while (rejectedNote.transform.position.x > destinationX)
        {
            rejectedNote.transform.Translate(Vector3.left * noteMoveSpeed * Time.deltaTime);
            yield return null;
        }

        while (maxAccept <= 4)
        {
            RejectNote(0.5f);
        }

        // Delay before moving the next note
        yield return new WaitForSeconds(noteDelay);


        Destroy(rejectedNote);
    }

    private int RejectNote(float chance)
    {
        if (Random.value <= chance)
        {
            GameObject rejectedNote = Instantiate(rejectedNotePrefab, rejectionPosition.position, rejectionPosition.rotation);

            // Disable Rigidbody initially so that it doesn't fall
            Rigidbody rigidbody = rejectedNote.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.isKinematic = true;
            }

            return 1;
        }

        return 0;
    }
}
