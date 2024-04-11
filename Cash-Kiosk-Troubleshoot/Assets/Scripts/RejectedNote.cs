using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RejectedNote : MonoBehaviour
{
    public void EnableRigidBody(SelectExitEventArgs args)
    {
        GameObject note = (args.interactableObject as XRBaseInteractable).gameObject;

        Rigidbody rigidbody = note.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
        }
    }

}
