using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyholeController : MonoBehaviour
{
    public Quaternion correctRotation;
    public Vector3 correctPosition;

    private bool isKeyInserted = false;

    private void OnCollisionEnter(Collision collision)
    {
        KeyController keyController = collision.gameObject.GetComponent<KeyController>();

        keyController.transform.rotation = Quaternion.Euler(0, 180, 0);
        keyController.transform.position = new Vector3(-4.7331f, 1.5655f, 7.7414f);

        keyController.DisableRigidbody();
        isKeyInserted = true;
    }

    public bool IsKeyInserted()
    {
        return isKeyInserted;
    }
}
