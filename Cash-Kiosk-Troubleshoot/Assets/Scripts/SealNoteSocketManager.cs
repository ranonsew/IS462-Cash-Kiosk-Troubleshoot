using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SealNoteSocketManager : XRSocketInteractor
{
    public string[] targetTag;
    public GameObject rejectedNotePrefab;
    public Transform sealPosition;

    private Vector3 originalPosition;
    private float noteDelay = 0.1f;
    private int sealedNoteCount = 0;

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

        // Start moving notes
        StartCoroutine(MoveRejectedNote(rejectedNote));

    }

    IEnumerator MoveRejectedNote(GameObject rejectedNote)
    {
        // move to seal location
        rejectedNote.transform.position = sealPosition.position;
        sealedNoteCount++;

        // Delay before moving the next note
        yield return new WaitForSeconds(noteDelay);
    }
}
