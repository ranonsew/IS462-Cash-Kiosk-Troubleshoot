using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyLockManager : MonoBehaviour
{
    public Vector3 relativePosition;
    public Quaternion relativeRotation;
    public bool inplaced = false;
    //public Animator animator;

    //private void Start()
    //{
    //    animator = GetComponent<Animator>();
    //    animator.speed = 0f;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Keyhole") && !inplaced)
        {

            string objectName = collision.gameObject.name;
            Debug.Log(objectName);
            transform.parent = collision.gameObject.transform;

            if (objectName == "Alarm_keyhole")
            {
                relativePosition = new Vector3(-0.002050567f, 0.0005108925f, -0.01125574f);
                relativeRotation = Quaternion.Euler(90, 0, 0);
            }
            else if (objectName == "Kiosk_keyhole")
            {
                relativePosition = new Vector3(0.00134669f, -0.002152986f, -0.009987785f);
                relativeRotation = Quaternion.Euler(90, 0, 0);
            }
            else if (objectName == "Coins_keyhole")
            {
                transform.position = new Vector3(-3.707997e-05f, -0.0005572594f, -0.02138846f);
                transform.rotation = Quaternion.Euler(90, 180, 180);
            }

            //Debug.Log(transform.position);
            //Debug.Log(transform.rotation);
            transform.position = collision.gameObject.transform.TransformPoint(relativePosition);
            transform.rotation = collision.gameObject.transform.rotation * relativeRotation;

            //animator.SetTrigger("Key_LeftToRight");

            inplaced = true;
        }
    }

}
