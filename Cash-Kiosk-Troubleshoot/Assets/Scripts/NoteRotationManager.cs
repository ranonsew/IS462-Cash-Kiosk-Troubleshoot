using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class NoteRotationManager : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private XRBaseController interactorController;
    private bool canRotateX = true;

    void Start()
    {
        // Get the XRGrabInteractable component attached to this GameObject
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Subscribe to the selectEntered and selectExited events
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {

        if (args.interactorObject is XRBaseControllerInteractor controllerInteractor && controllerInteractor != null)
        {
            interactorController = controllerInteractor.xrController;
        }

        // The object is now grabbed
        Debug.Log(gameObject.name + " is being grabbed");
    }

    void OnReleased(SelectExitEventArgs args)
    {
        // The object is released
        interactorController = null;
        Debug.Log(gameObject.name + " is released");
    }

    // Rotate the note
    private void Rotate(float angle, Vector3 axis)
    {
        transform.Rotate(axis, angle);
    }

    private void Update()
    {
        // Check if button A is pressed on the controller
        if (interactorController != null && Input.anyKeyDown)
        {
            // Rotate the note along X axis if canRotateX is true
            if (canRotateX)
            {
                Rotate(180f, Vector3.right);
            }
            // Otherwise, rotate along Y axis
            else
            {
                Rotate(180f, Vector3.up);
            }
            // Toggle canRotateX for next rotation
            canRotateX = !canRotateX;
        }
    }
}