using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneC3DoorControllerReverse : MonoBehaviour
{
    private HingeJoint hingeJoint;
    private bool isOpen = false;
    private float closedAngle = 0f;
    private float openAngle = -90f;
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
        if (isOpen)
        {
            SetDoorAngle(closedAngle);
            isOpen = false;
        }
        else
        {
            SetDoorAngle(openAngle);
            isOpen = true;
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
