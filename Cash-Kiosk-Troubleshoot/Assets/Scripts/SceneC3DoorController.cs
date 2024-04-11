using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneC3DoorController : MonoBehaviour
{
    private HingeJoint hingeJoint;
    private bool isOpen = false;
    private float closedAngle = -90f;
    private float openAngle = 90f;
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
        }
        else
        {
            SetDoorAngle(openAngle);
        }
    }

    private void SetDoorAngle(float angle)
    {
        JointMotor motor = hingeJoint.motor;
        motor.force = 100;
        //motor.targetVelocity = isOpen ? -100 : 100;
        motor.targetVelocity = 100;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;

        JointLimits limits = hingeJoint.limits;
        limits.min = angle - 5;
        limits.max = angle + 5;
        hingeJoint.limits = limits;
        hingeJoint.useLimits = true;
    }
}
