using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public GameObject ballObject;
    public Material[] materials = new Material[2]; //for 3 materials
    // Start is called before the first frame update
    private int index = 0;
    public Rigidbody rigidbody_kiosk_front_door;
    public Rigidbody rigidbody_notes_front_door;

    public Rigidbody rigidbody_notes_internal_door;
    public TMPro.TMP_Text messageText;

    public int totalStepsSceneC = 3;
    public int currentStepsSceneC;

    void Start()
    {
        // // Set color as Random here upon clicking it will be blue as a way of fixing the lights:
        int color = Random.Range(0, 2);
        index = color;
        Debug.Log("color"+ index.ToString());
        currentStepsSceneC = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeMaterial(){
        Debug.Log("Change material triggered");
        index = 1;
        ballObject.GetComponent<MeshRenderer>().material = materials[1];
        checkSteps();
}

    public void openKioskDoor(){
        Debug.Log("openKioskDoor adding torque");
        rigidbody_kiosk_front_door.AddTorque(Vector3.up * 1000);
        currentStepsSceneC += 1;
    }

    public void openNotesDoor(){
        Debug.Log("openNotesDoor adding torque");
        rigidbody_notes_front_door.AddTorque(Vector3.up * 1000);
        currentStepsSceneC += 1;
    }

    public void openNotesInternalDoor(){
        Debug.Log("openNotesInternalDoor adding torque");
        rigidbody_notes_internal_door.AddTorque(Vector3.up * 1000);
        currentStepsSceneC += 1;
    }

    public void checkSteps(){
        messageText.SetText("No errors");
        messageText.color = Color.green;
        PointsManager.instance.updateScoreCompletion("SceneC", (currentStepsSceneC/totalStepsSceneC));
        Debug.Log("completionRate: " + PointsManager.instance.points[0][0]);
        
    }




}
