using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneC3DoorController_Notes : MonoBehaviour
{
    private HingeJoint hingeJoint;
    private bool isOpen = false;
    private float closedAngle = 0f;
    public float openAngle = 150f;
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
        SceneCvariables.instance.NotesDoorCheck = true;

        Debug.Log("SceneCvariables.instance.InternalNotesDoorOpen: " + SceneCvariables.instance.InternalNotesDoorOpen);
        if (isOpen & SceneCvariables.instance.InternalNotesDoorOpen == false & SceneCvariables.instance.KioskDoorOpen)
        {
            SetDoorAngle(closedAngle);
            isOpen = false;
            SceneCvariables.instance.NotesDoorOpen = false;

        }
        else
        {
            SetDoorAngle(openAngle);
            isOpen = true;
            SceneCvariables.instance.NotesDoorOpen = true;

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
