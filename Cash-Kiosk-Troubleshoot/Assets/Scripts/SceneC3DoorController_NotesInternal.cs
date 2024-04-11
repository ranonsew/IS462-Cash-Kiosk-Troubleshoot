using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneC3DoorController_NotesInternal : MonoBehaviour
{
    private HingeJoint hingeJoint;
    private bool isOpen = false;
    private float closedAngle = 0f;
    public float openAngle = 110f;
    private XRSimpleInteractable xrInteractable;

    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        xrInteractable = GetComponent<XRSimpleInteractable>();

        xrInteractable.selectEntered.AddListener(DoorSwingWrapper);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoorSwingWrapper(SelectEnterEventArgs args)
    {

        SceneCvariables.instance.InternalNotesDoorCheck = true;
        if (isOpen & SceneCvariables.instance.NotesDoorOpen & SceneCvariables.instance.KioskDoorOpen)
        {
            SetDoorAngle(closedAngle);
            isOpen = false;
            SceneCvariables.instance.InternalNotesDoorOpen = false;

        }
        else
        {
            SetDoorAngle(openAngle);
            isOpen = true;
            SceneCvariables.instance.InternalNotesDoorOpen = true;
        }
    }

    private void SetDoorAngle(float angle)
    {
        JointMotor motor = hingeJoint.motor;
        motor.force = 100;
        motor.targetVelocity = isOpen ? -100 : 100;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;

        JointLimits limits = hingeJoint.limits;
        limits.min = angle - 10;
        limits.max = angle + 10;
        hingeJoint.limits = limits;
        hingeJoint.useLimits = true;
    }
}
