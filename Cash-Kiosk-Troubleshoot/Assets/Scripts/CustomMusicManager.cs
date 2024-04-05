using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CustomMusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip[] clips; // drag and add audio clips in the inspector
    AudioSource audioSource;
    private int clipIndex = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // public void PlayNext(){
    //     Debug.Log("Music manager called!");
    //     if (clipIndex >= clips.Length - 1){
    //             clipIndex = 0;
    //         }else{ clipIndex++; }
    //         audioSource.clip = clips[clipIndex];
    //         audioSource.Play();
    // }
}
