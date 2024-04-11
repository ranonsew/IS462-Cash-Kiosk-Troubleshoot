using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneDChangeInstruction : MonoBehaviour
{
    public InstructionManager instructionManager;
    private bool stackedDone = false;

    void Start()
    {
        StartCoroutine(nextInstructionWithTimer(7f));
    }

    private void Update()
    {
        if (!stackedDone)
        {
            findStacked();
        }
    }

    public IEnumerator nextInstructionWithTimer(float number)
    {
        yield return new WaitForSeconds(number);
        instructionManager.LoadNextInstructions();
    }

    public void findStacked()
    {
        GameObject[] stackedNotesTag = GameObject.FindGameObjectsWithTag("Notes");

        if (stackedNotesTag.Length == 0)
        {
            instructionManager.LoadNextInstructions();
            stackedDone = true;
        }
    }
}
