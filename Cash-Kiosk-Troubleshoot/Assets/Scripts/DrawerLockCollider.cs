using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class DrawerLockCollider : MonoBehaviour
{
    public ScenarioFNoteController scenarioFController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// On trigger enter stuff for unlocking/locking allowance
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Drawer Trigger tag: {other.gameObject.tag}");
        if (other.gameObject.CompareTag("RecyclerHead"))
        {
            scenarioFController.allowLock = true;
            scenarioFController.fillerCube.SetActive(false);
            scenarioFController.instructionManager.LoadSpecificInstructionIndex(3); // If the note is hard to spot ...
        }
        Debug.Log($"Lock state: {scenarioFController.allowLock}");
        if (other.gameObject.CompareTag("RecyclerDrawer"))
        {
            scenarioFController.fillerCube.SetActive(!scenarioFController.fillerCube.activeSelf);
        }
    }

    /// <summary>
    /// On collision for testing
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Drawer Collision tag: {collision.gameObject.tag}");
        if (collision.gameObject.CompareTag("RecyclerHead"))
        {
            Debug.Log("Lock enabled, cannot open, stop!");
        }
    }
}
