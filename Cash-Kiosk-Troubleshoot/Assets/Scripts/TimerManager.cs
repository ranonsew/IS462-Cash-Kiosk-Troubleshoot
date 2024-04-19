using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 10;
    public float timeAllowed = 10;
    public float totalTime = 0;
    public TMP_Text messageText;
    public bool triggered = false;
    public GameObject npc;
    public Transform npcSpawnLocation;

    public string sceneTitle; // for points manager instance

    // Start is called before the first frame update
    void Start()
    {
        //  Debug.Log(PointsManager.instance.points.ToString());
{
    // do something with entry.Value or entry.Key
}
    }

    void Update()
    {

        if (PointsManager.instance != null){
            PointsManager.instance.updateScore(sceneTitle, "timeInSec", (float) totalTime/1000); // previously "SceneC"
        }
        
        if (timeRemaining > 0 + Time.deltaTime)
        {
            timeRemaining -= Time.deltaTime;
            messageText.SetText(timeRemaining.ToString("F2"));


            // Trigger npc only once:
            if (triggered == false & timeRemaining < timeAllowed){

                triggered = true;
                TriggerNPC();
            }
        }else{
            // Debug.Log("Updating time here for score!");
            
        }
        totalTime += Time.deltaTime;
    }

    void TriggerNPC(){
        // Debug.Log("Scolding NPC triggered");
        GameObject b = Instantiate(npc, npcSpawnLocation.position, npcSpawnLocation.rotation);
    }
}
