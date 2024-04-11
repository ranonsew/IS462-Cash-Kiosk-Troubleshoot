using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using BNG.ToolTip;
public class InstructionManager : MonoBehaviour
{ // Start is called before the first frame update
    [SerializeField] AudioClip[] clips; // drag and add audio clips in the inspector
    AudioSource audio;
    private int clipIndex = 0;
    public TextMeshPro displayInstructions;
    public string[] instructions = new string[]{"Hello welcome to scene C. There's an error. Please fix!", 
    "Open all the doors and check if all of them are closed inside", 
    "Remember to close all of the doors. check the screen for errors and see if the blue lights are yellow. "};

    // public ToolTip a; // you will need this if scriptB is in another GameObject
    //                  // if not, you can omit this
    //                  // you'll realize in the inspector a field GameObject will appear
    //                  // assign it just by dragging the game object there
    // public Tooltip script; // this will be the container of the script

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = clips[clipIndex];
        displayInstructions.text = instructions[clipIndex];
        clipIndex++;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadNextInstructions(){
        if (clipIndex < instructions.Length & clipIndex < clips.Length ){

        Debug.Log("loading next instructions");
        displayInstructions.text = instructions[clipIndex];
            //audio = GetComponent<AudioSource>();
            audio.clip = clips[clipIndex];
        audio.Play();

        // change tooltip location here:
        // script = a.getComponent<Tooltip>();
        // Debug.log("tooptip transform point: " + "");


        clipIndex++;
        }
    }

    /// <summary>
    /// Load specific instruction via array index
    /// </summary>
    /// <param name="index">array index</param>
    public void LoadSpecificInstructionIndex(int index)
    {
        if (index >= 0 & index < instructions.Length)
        {
            Debug.Log($"Loading specified instruction at index: {index}");
            displayInstructions.text = instructions[index];
            audio.clip = clips[index];
            audio.Play();
        }
    }
}
