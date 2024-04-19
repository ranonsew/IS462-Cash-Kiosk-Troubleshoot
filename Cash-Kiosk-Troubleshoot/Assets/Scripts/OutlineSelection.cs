using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Outline;
public class OutlineSelection : MonoBehaviour
{
    // public Transform highlight;
    public Transform kioskDoor;
    public Transform notesDoor;
    public Transform notesInternalDoor;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneCvariables.instance != null){
            if (SceneCvariables.instance.KioskDoorOpen == true){
                // Debug.Log("addinging object outline for kiosk");
                addOutline(kioskDoor);
                // Debug.Log("finished object outline for kiosk");
            }else{ removeOutline(kioskDoor); }

            if (SceneCvariables.instance.NotesDoorOpen == true){
                addOutline(notesDoor);
            }else{ removeOutline(notesDoor); }

            if (SceneCvariables.instance.InternalNotesDoorOpen == true){
                addOutline(notesInternalDoor);
            }else{ removeOutline(notesInternalDoor); }

        }else{
            // Debug.Log("SceneC3Variables not even inside");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneCvariables.instance != null){
            if (SceneCvariables.instance.KioskDoorOpen == true){
                addOutline(kioskDoor);
            }else{ removeOutline(kioskDoor); }

            if (SceneCvariables.instance.NotesDoorOpen == true){
                addOutline(notesDoor);
            }else{ removeOutline(notesDoor); }

            if (SceneCvariables.instance.InternalNotesDoorOpen == true){
                addOutline(notesInternalDoor);
            }else{ removeOutline(notesInternalDoor); }
        }
    }


    public void addOutline(Transform obj){
         if (obj.gameObject.GetComponent<Outline>() == null){
            Outline outline = obj.gameObject.AddComponent<Outline>();
            outline.enabled = true;
            }else{
                obj.gameObject.GetComponent<Outline>().enabled = true;
                }
            obj.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
            obj.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
    }

    public void removeOutline(Transform obj){
        if (obj.gameObject.GetComponent<Outline>() == null){
            Outline outline = obj.gameObject.AddComponent<Outline>();
            outline.enabled = true;
        }
        // obj.gameObject.GetComponent<Outline>().enabled = false;
        obj.gameObject.GetComponent<Outline>().OutlineColor = Color.green;
        obj.gameObject.GetComponent<Outline>().OutlineWidth = 4.0f;
    }


}
