using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.SceneManagement;
public class SceneC3DoorController : MonoBehaviour
{
    private HingeJoint hingeJoint;
    private bool isOpen = false;
    private float closedAngle = 0f;
    public float openAngle = 160f;
    public TextMeshPro instructions;
    public TextMeshPro errorMessage;
        public ParticleSystem p;
    public Transform pLocation;
    public InstructionManager m;
    // public GameObject lights;
    // public Material[] materials;
    // public bool blueOrYellow = true;

    private XRSimpleInteractable xrInteractable;

    
    // Start is called before the first frame update
    void Start()
    {
        p = Instantiate(p, pLocation.position, pLocation.rotation);
            var emission = p.emission; // Stores the module in a local variable
            emission.enabled = false; // Applies the new value directly to the Particle System}

        hingeJoint = GetComponent<HingeJoint>();
        xrInteractable = GetComponent<XRSimpleInteractable>();

        xrInteractable.selectEntered.AddListener(DoorSwingWrapper);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void DoorSwingWrapper(SelectEnterEventArgs args)
    {
        SceneCvariables.instance.KioskDoorCheck = true;
        Debug.Log("SceneCvariables.instance.InternalNotesDoorOpen: " + SceneCvariables.instance.InternalNotesDoorOpen);
        if (isOpen & SceneCvariables.instance.NotesDoorOpen == false & SceneCvariables.instance.InternalNotesDoorOpen == false)
        {
            SetDoorAngle(closedAngle);
            isOpen = false;
            SceneCvariables.instance.KioskDoorOpen = false;
            // if all closed then trigger ending scene here and confetti!: IF not show on instruction to recheck all the doors
            if (SceneCvariables.instance.KioskDoorOpen == false & SceneCvariables.instance.NotesDoorOpen == false & 
            SceneCvariables.instance.InternalNotesDoorOpen == false){
                // m.LoadNextInstructions();
                // PointsManager.instance.waitLoadResultsScene();
                Debug.Log("Yyyyyy");
                PointsManager.instance.updateScore("SceneC", "completionRate", (1));
                PointsManager.instance.updateScore("SceneC", "numErrors", (float) (SceneCvariables.instance.KioskDoorCheck ? 0: 1)  + 
                (SceneCvariables.instance.NotesDoorCheck ? 0 : 1) + (SceneCvariables.instance.InternalNotesDoorCheck ? 0 :1));
                errorMessage.text = "Resolved!";
                errorMessage.color = Color.green;
                var emission = p.emission; // Stores the module in a local variable
                emission.enabled = true; // Applies the new value directly to the Particle System}

                // PointsManager.instance.waitLoadResultsScene();
                waitLoadResultsScene();
            
            }else{

                instructions.text = "Please find all loose doors and close it properly.";
            }
        }
        else
        {
            SetDoorAngle(openAngle);
            isOpen = true;
            SceneCvariables.instance.KioskDoorOpen = true;
        }

        SceneCvariables.instance.start = false;
    }

    private void SetDoorAngle(float angle)
    {
        JointMotor motor = hingeJoint.motor;
        motor.force = 100;
        motor.targetVelocity = isOpen ? -100 : 100;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;

        JointLimits limits = hingeJoint.limits;
        limits.min = angle - 10;
        limits.max = angle + 10;
        hingeJoint.limits = limits;
        hingeJoint.useLimits = true;
    }


        public void waitLoadResultsScene(){
            StartCoroutine(loadResultsScene());
        }

        IEnumerator loadResultsScene(){
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene("SceneResults");
        }
}
