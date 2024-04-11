using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeySocketManager : XRSocketInteractor
{
    public string[] targetTag;

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
        XRBaseInteractable XRBaseInteractable = args.interactableObject as XRBaseInteractable;
        Destroy(XRBaseInteractable.gameObject);
    }

}
