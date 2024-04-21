using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCollectionInstructions : MonoBehaviour
{
    public InstructionManager instructionManager;

    private void Start()
    {
        StartCoroutine(nextInstructionWithTimer(4f));
    }

    public void callInstruction()
    {
        instructionManager.LoadNextInstructions();
    }

    public void callInstructionsAfterPassword()
    {
        StartCoroutine(nextInstructionWithTimer(8f));
    }

    public IEnumerator nextInstructionWithTimer(float number)
    {
        yield return new WaitForSeconds(number);
        instructionManager.LoadNextInstructions();
    }
}
