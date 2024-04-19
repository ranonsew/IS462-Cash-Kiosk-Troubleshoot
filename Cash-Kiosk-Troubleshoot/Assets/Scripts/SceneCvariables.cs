using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneCvariables : MonoBehaviour
{
    public bool KioskDoorOpen = true;
    public bool NotesDoorOpen = true;
    public bool InternalNotesDoorOpen = false;

    public bool KioskDoorCheck = true;
    public bool NotesDoorCheck = false;
    public bool InternalNotesDoorCheck = false;
    public bool start = true;
    #region Init / Gestion du singleton
    public static SceneCvariables instance;
    #endregion

    void Start(){

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);



        // if (KioskDoorOpen == true){
        //     Debug.Log("Helloooooo");
        // }else{
        //      Debug.Log("Nooooo");
        // }

    }

    


}