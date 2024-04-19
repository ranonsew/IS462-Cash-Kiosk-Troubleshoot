using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEditor.Localization.Plugins.XLIFF.V12;
public class SealNoteSocketManager : XRSocketInteractor
{
    public string[] targetTag;
    public GameObject rejectedNotePrefab;
    public Transform sealPosition;
    public GameObject sealBag;

    private bool sceneCompleted = false;
    private Vector3 originalPosition;
    private float noteDelay = 0.1f;
    private int sealedNoteCount = 0;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }



    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    private bool MatchUsingTag(IXRInteractable interactable)
    {
        XRBaseInteractable XRBaseInteractable = interactable as XRBaseInteractable;
        if (XRBaseInteractable == null) return false;

        bool tagMatch = false;
        foreach (string tag in targetTag)
        {
            tagMatch = tagMatch || XRBaseInteractable.CompareTag(tag);
        }
        return tagMatch;
    }

    public void DestroyPlacedObject(SelectEnterEventArgs args)
    {
        // Start the coroutine for delaying
        StartCoroutine(DelayAndDestroy(args.interactableObject as XRBaseInteractable));
    }

    private IEnumerator DelayAndDestroy(XRBaseInteractable objToDestroyXRBaseInteractable)
    {
        // update score


        yield return new WaitForSeconds(0.2f);

        // Now continue with the destruction process
        GameObject objToDestroy = objToDestroyXRBaseInteractable.gameObject;
        originalPosition = objToDestroy.transform.position;
        Destroy(objToDestroyXRBaseInteractable.gameObject);
    }


        public void waitLoadResultsScene(){
            StartCoroutine(loadResultsScene());
        }

        IEnumerator loadResultsScene(){
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene("SceneResults");
        }

    public void beforeCreateObject(SelectEnterEventArgs args){
        CreateObject(args);
        // sceneCompleted = checkCompletion();
        // Debug.Log("Checking completion now!");
        // if (sceneCompleted) {

        //     Debug.Log("Scene compelted~!");
        //     PointsManager.instance.updateScore("SceneD", "completionRate", (1));
        //     PointsManager.instance.updateScore("SceneD", "numErrors", (float) 0);
        //     waitLoadResultsScene();
        // }

    }

    public void CreateObject(SelectEnterEventArgs args)
    {
        // if (PointsManager.instance != null){
        //     PointsManager.instance.sceneTitle = "SceneD";
        // }
        // Start the coroutine for delaying
        StartCoroutine(DelayAndCreate(args));


    }

    public IEnumerator DelayAndCreate(SelectEnterEventArgs args)
    {
        yield return new WaitForSeconds(0.25f);

        GameObject rejectedNote = Instantiate(rejectedNotePrefab, originalPosition, Quaternion.identity);

        // Remove XR Grab Interactable component from the instantiated object
        XRGrabInteractable grabInteractable = rejectedNote.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            Destroy(grabInteractable);
        }

        // disable physics on the instantiated object if it's not needed
        Rigidbody rb = rejectedNote.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }



        // Start moving notes
        StartCoroutine(MoveRejectedNote(rejectedNote));




    }

    IEnumerator MoveRejectedNote(GameObject rejectedNote)
    {
        // move to seal location
        rejectedNote.transform.position = sealPosition.position;
        rejectedNote.transform.parent = sealBag.transform;
        sealedNoteCount++;
        Debug.Log("MoveRejectedNote!");

        // check if scene is complete, ie all rejected notes in seal bag
        // sceneCompleted = checkCompletion();

        //if (sceneCompleted)
        //{

        //    PointsManager.instance.updateScore("SceneD", "numErrors", countMistake());
        //    PointsManager.instance.updateScore("SceneD", "completionRate", 100);
        //}

        sceneCompleted = checkCompletion();
        Debug.Log("Checking completion now!");
        if (sceneCompleted) {
            Debug.Log("Scene compelted~!");
            PointsManager.instance.updateScore("SceneD", "completionRate", (1));
            PointsManager.instance.updateScore("SceneD", "numErrors", (float) countMistake());
            waitLoadResultsScene();
        }

        // Delay before moving the next note
        yield return new WaitForSeconds(noteDelay);
    }

    private bool checkCompletion()
    {
        // check if all notes in seal bag
        // Find all GameObjects with the name "rejectedNote" in the scene
        GameObject[] rejectedNotes = GameObject.FindGameObjectsWithTag("RejectedNote");

        // Find all GameObjects with the name "StackOfNotesWSocket" in the scene
        GameObject[] stackOfNotes = GameObject.FindGameObjectsWithTag("Notes");
        if (stackOfNotes.Length > 0)
        {
            PointsManager.instance.updateScore("SceneD", "numErrors", (float) 1);
            return false;
        }

        // Iterate through each rejectedNote
        foreach (GameObject note in rejectedNotes)
        {
            // Check if the note has no parent
            if (note.transform.parent == null)
            {
                sceneCompleted = false;
                break;
            }
            else
            {
                sceneCompleted = true;
            }
        }
        return sceneCompleted;
    }

    private int countMistake()
    {
        int count = 0;

        // Find all GameObjects with the name "StackOfNotesWSocket" in the scene
        GameObject[] stackOfNotes = GameObject.FindGameObjectsWithTag("Notes");

        if (stackOfNotes.Length > 0)
        {
            count++;
        }

        foreach (bool value in NotesSocketWithTagCheck.rotationDict.Values)
        {
            if (!value)
            {
                count++;
                break;
            }
        }

        return count;
    }
}
