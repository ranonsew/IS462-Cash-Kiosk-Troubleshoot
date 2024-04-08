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
        Debug.Log("Drawer Collided with back");
        scenarioFController.LockDrawer();
    }
}
