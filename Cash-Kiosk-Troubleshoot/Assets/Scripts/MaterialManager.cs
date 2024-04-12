using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI.BodyUI;
using TMPro;

// #pragma strict
// @script RequireComponent(AudioSource)
public class MaterialManager : MonoBehaviour
{
    public GameObject ballObject;
    public Material[] materials = new Material[2]; //for 3 materials
    // Start is called before the first frame update
    private int index = 0;
    public Rigidbody rigidbody_kiosk_front_door;
    public Rigidbody rigidbody_notes_front_door;

    public Rigidbody rigidbody_notes_internal_door;
    public Rigidbody rigidbody_internal_slit;
    public TMPro.TMP_Text messageText;

    public int totalStepsSceneC = 7;
    public int currentStepsSceneC;
    public bool internalDoorOpen = false;
    public bool notesDoorOpen = false;
    public bool kioskDoorOpen = false;
    public bool internalSlitOpen = false;
    public Transform originalKioskLocation;
    public Transform originalNotesDoor;

    public GameObject lights;
    public GameObject lights2;
    public Material[] materials2;

    [SerializeField]
    Animator doorAnim;

    [SerializeField]
    Animator notesDoorAnim;

    bool canOpen;
    bool canOpenNotes;

    InstructionManager m;

    void Start()
    {
        // // Set color as Random here upon clicking it will be blue as a way of fixing the lights:
        int color = Random.Range(0, 2);
        index = color;
        Debug.Log("color"+ index.ToString());
        currentStepsSceneC = 0;
        m = GetComponent<InstructionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneCvariables.instance != null){
        if (SceneCvariables.instance.KioskDoorOpen == false & SceneCvariables.instance.NotesDoorOpen == false & 
            SceneCvariables.instance.InternalNotesDoorOpen == false & SceneCvariables.instance.start == false){
                lights.GetComponent<MeshRenderer>().material = materials[1];
                lights2.GetComponent<MeshRenderer>().material = materials[1];
            }
        }
    }

    
    public void operateKioskDoor(){
        if (kioskDoorOpen){
            closeKioskDoor();
            kioskDoorOpen = !kioskDoorOpen;
        }else{
            openKioskDoor();
            kioskDoorOpen = !kioskDoorOpen;
        }
    }

    public void cubematch(){
        Debug.Log("cubeematchhhh");
        rigidbody_kiosk_front_door.AddTorque(Vector3.down * 1000);
        Debug.Log("cubeematchhhh2");
    }


    public void openKioskDoor(){
        Debug.Log("openKioskDoor adding torque");
        rigidbody_kiosk_front_door.AddTorque(Vector3.up * 3000);
        currentStepsSceneC += 1;
    }

    public void closeKioskDoor(){
        Debug.Log("closeKioskDoor adding torque");
        rigidbody_kiosk_front_door.AddTorque(Vector3.down * 1000);
        currentStepsSceneC += 1;


        // Follow these stops at the end:
        // SceneC, SceneD, SceneE, SceneF
// // completionRate, numErrors, timeInSec, overall
// updateScore(string sceneName, string metric, int metricRate)
// currentStepsSceneC/totalStepsSceneC
        PointsManager.instance.updateScore("SceneC", "completionRate", (1));
        Debug.Log("completionRate: " + PointsManager.instance.points[0][0]);

    }

    public void operateNotesDoor(){
        if (notesDoorOpen){
            closeNotesDoor();
            notesDoorOpen = !notesDoorOpen;
        }else{
            openNotesDoor();
            notesDoorOpen = !notesDoorOpen;
        }
    }

    public void openNotesDoor(){
        Debug.Log("openNotesDoor adding torque");
        rigidbody_notes_front_door.AddTorque(Vector3.up * 1000);
        currentStepsSceneC += 1;
    }

    public void closeNotesDoor(){
        Debug.Log("closeNotesDoor adding torque");
        rigidbody_notes_front_door.AddTorque(Vector3.down * 1000);
        currentStepsSceneC += 1;
    }

    public void operateNotesInternalDoor(){
        if (internalDoorOpen){
            closeNotesInternalDoor();
            internalDoorOpen = !internalDoorOpen;
        }else{
            openNotesInternalDoor();
            internalDoorOpen = !internalDoorOpen;
        }
    }


    public void openNotesInternalDoor(){
        Debug.Log("openNotesInternalDoor adding torque");
        rigidbody_notes_internal_door.AddTorque(Vector3.up * 1000);
        currentStepsSceneC += 1;
        
    }

    public void closeNotesInternalDoor(){
        Debug.Log("closeNotesInternalDoor adding torque");
        rigidbody_notes_internal_door.AddTorque(Vector3.down * 1000);
        currentStepsSceneC += 1;
    }

        public void operateInternalSlit(){
        if (internalSlitOpen){
            closeInternalSlit();
            notesDoorOpen = !notesDoorOpen;
        }else{
            openInternalSlit();
            notesDoorOpen = !notesDoorOpen;
        }
    }

    public void openInternalSlit(){
        Debug.Log("openInternalSlit adding torque");
        rigidbody_internal_slit.AddTorque(Vector3.up * 1000);
        currentStepsSceneC += 1;
    }

    public void closeInternalSlit(){
        Debug.Log("closeInternalSlit adding torque");
        rigidbody_internal_slit.AddTorque(Vector3.down * 1000);
        currentStepsSceneC += 1;
    }


    public void checkSteps(){
        messageText.SetText("No errors");
        messageText.color = Color.green;
        currentStepsSceneC += 8;
        
    }



// end of all official closing mehtods:




    public void operateKioskDoor2(){
        originalKioskLocation = rigidbody_kiosk_front_door.transform;
        Debug.Log("operateKioskDoor2 position: " + originalKioskLocation.position);
        // Debug.Log(rigidbody_kiosk_front_door.angularVelocity);
         if (kioskDoorOpen){

            Debug.Log("originalKioskLocation.position: " + originalKioskLocation.position);
            rigidbody_kiosk_front_door.transform.position = originalKioskLocation.position;
            closeKioskDoor2();
            kioskDoorOpen = !kioskDoorOpen;
        }else{
            openKioskDoor2();
            kioskDoorOpen = !kioskDoorOpen;
        }
    }

        public void openKioskDoor2(){
            // originalKioskLocation = rigidbody_kiosk_front_door.transform;
            m.LoadNextInstructions();
        Debug.Log("openKioskDoor adding torque");
        // rigidbody_kiosk_front_door.AddTorque(Vector3.up * 1000);
        currentStepsSceneC += 1;
    }

    public void closeKioskDoor2(){
        m.LoadNextInstructions();
        Debug.Log("closeKioskDoor adding torque");
        // rigidbody_kiosk_front_door.transform = originalKioskLocation;
        // rigidbody_kiosk_front_door.AddTorque(Vector3.down * 1000);
        currentStepsSceneC += 1;
        // Follow these stops at the end:
        // SceneC, SceneD, SceneE, SceneF
// // completionRate, numErrors, timeInSec, overall
// updateScore(string sceneName, string metric, int metricRate)
// currentStepsSceneC/totalStepsSceneC
        // PointsManager.instance.updateScore("SceneC", "completionRate",  100);
        // PointsManager.instance.updateScore("SceneC", "numErrors", 2);
        // // PointsManager.instance.updateScore("SceneC", "timeInSec", (float) 22.2);

        // PointsManager.instance.updateScore("SceneC", "overall", 3000);
        // Debug.Log("completionRate: " + PointsManager.instance.points[0][0]);

    }

    public void goResultsPageTest(){
    PointsManager.instance.updateScore("SceneC", "completionRate", 1);
}

    public void changeMaterial(){
        Debug.Log("Change material triggered");
        index = 1;
        ballObject.GetComponent<MeshRenderer>().material = materials[1];
        checkSteps();
}

        public void operateNotesDoor2(){
            originalNotesDoor = rigidbody_notes_front_door.transform;
        if (notesDoorOpen){
            rigidbody_notes_front_door.transform.position = originalNotesDoor.position;
            // closeNotesDoor();
            notesDoorOpen = !notesDoorOpen;
        }else{
            // openNotesDoor();
            notesDoorOpen = !notesDoorOpen;
        }
    }

    public void SetKioskDoor(){
        canOpen = !canOpen;
        doorAnim.SetBool("isOpen", canOpen);
    }

    public void SetNotesDoor(){
        canOpenNotes = !canOpenNotes;
        notesDoorAnim.SetBool("isOpen", canOpenNotes);
    }



}
