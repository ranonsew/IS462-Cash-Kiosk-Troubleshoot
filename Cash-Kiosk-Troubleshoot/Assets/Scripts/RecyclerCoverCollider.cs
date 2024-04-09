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
        Debug.Log($"Cover Collision tag: {collision.gameObject.tag}");
        Debug.Log("Drawer Collided with back");
        if (collision.gameObject.CompareTag("RecyclerDrawer")) {
            scenarioFController.LockDrawer();
        }
        
    }
}
