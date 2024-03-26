using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 10;
    public TMP_Text messageText;
    public bool WinnerTriggered = false;
    // public GameObject winnieNPC;
    // private Animator animator;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            messageText.SetText(timeRemaining.ToString("F2"));


            // Trigger winnie only once:
            if (WinnerTriggered == false & timeRemaining < 10){
                WinnerTriggered = true;
                TriggerWinner();
            }
        }else{
            Debug.Log("Time has run out!");
        }
    }

    void TriggerWinner(){
        Debug.Log("WinnieTriggered");
        // animator = GetComponent<Animator>(); 
        // winnieNPC = GetComponent<GameObject>(); 
        // animator = winnieNPC.GetComponent<Animator>();
        // winnieNPC.GetComponent<Animator>().Play("Defeated");
        // // WinnieNPCController.showDefeated();
    }
}
