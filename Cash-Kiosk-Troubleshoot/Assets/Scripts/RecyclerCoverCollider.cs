using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclerCoverCollider : MonoBehaviour
{
    public ScenarioFNoteController scenarioFController;

    /// <summary>
    /// Collision back thing
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RejectedNote"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<BoxCollider>());
        }

        if (collision.gameObject.CompareTag("RecyclerDrawer")) {
            scenarioFController.LockDrawer();
        }
    }
}
