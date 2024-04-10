using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InstructionManager : MonoBehaviour
{ // Start is called before the first frame update
    [SerializeField] AudioClip[] clips; // drag and add audio clips in the inspector
    AudioSource audio;
    private int clipIndex = 0;
    public TextMeshPro displayInstructions;
    public string[] instructions = new string[]{"Hello welcome to scene C. There is an error. Please try to fix it!", 
    "Open all the doors and check if all of them are closed inside", 
    "Remember to close all of the doors. check the screen for errors and see if the blue lights are yellow. "};
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


        Debug.Log("loading next instructions");
        audio = GetComponent<AudioSource>();
        audio.clip = clips[clipIndex];
        displayInstructions.text = instructions[clipIndex];
        clipIndex++;
        audio.Play();

    }


}
