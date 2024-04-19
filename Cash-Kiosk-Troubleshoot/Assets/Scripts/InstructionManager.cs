using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using BNG.ToolTip;
public class InstructionManager : MonoBehaviour
{ // Start is called before the first frame update
    public AudioClip[] clips; // drag and add audio clips in the inspector
    private AudioSource audio;
    private int clipIndex = 0;
    public TextMeshPro displayInstructions;
    public TextMeshProUGUI displayInstructionsUGUI;

    public string[] instructions = new string[]{"Hello! There's an error. Please open the white kiosk door below and 2 more doors.", 
    "Remember to close all of the doors. Check the screen for errors and see if the blue lights are yellow."};

    // public ToolTip a; // you will need this if scriptB is in another GameObject
    //                  // if not, you can omit this
    //                  // you'll realize in the inspector a field GameObject will appear
    //                  // assign it just by dragging the game object there
    // public Tooltip script; // this will be the container of the script

    void Start()
    {
        if (displayInstructions)
        {
            displayInstructions.text = instructions[clipIndex];
        }
        else if (displayInstructionsUGUI)
        {
            displayInstructionsUGUI.text = instructions[clipIndex];
        }

        audio = GetComponent<AudioSource>();
        audio.clip = clips[clipIndex];
        audio.Play();
        clipIndex++;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadNextInstructions(){
        if (clipIndex < instructions.Length & clipIndex < clips.Length ){

            Debug.Log("loading next instructions");
            if (displayInstructions)
            {
                displayInstructions.text = instructions[clipIndex];
            }
            else if (displayInstructionsUGUI)
            {
                displayInstructionsUGUI.text = instructions[clipIndex];
            }
            audio = GetComponent<AudioSource>();
            audio.clip = clips[clipIndex];
            audio.Play();

            // change tooltip location here:
            // script = a.getComponent<Tooltip>();
            // Debug.log("tooptip transform point: " + "");


            clipIndex++;
        }
    }

    /// <summary>
    /// Load specific instruction via array index. Check the instruction manager game object in the inspector for which index to use.
    /// </summary>
    /// <param name="index">array index</param>
    public void LoadSpecificInstructionIndex(int index)
    {
        if (index >= 0 & index < instructions.Length)
        {
            Debug.Log($"Loading specified instruction at index: {index}");
            if (displayInstructions)
            {
                displayInstructions.text = instructions[index];
            }
            else if (displayInstructionsUGUI)
            {
                displayInstructionsUGUI.text = instructions[index];
            }
            audio.clip = clips[index];
            audio.Play();
        }
    }
}
