 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInternalDoorAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator doorAnim;
    public bool clicked = false;
    public static bool isOpen = false;

    public void isClicked()
    {
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        if (!isOpen & !StorageManager.lockActive)
        {
            isOpen = true;
            doorAnim.SetBool("isOpen", true);
        }
        else if (isOpen & !StorageManager.lockActive)
        {
            isOpen = false;
            doorAnim.SetBool("isOpen", false);
        }
    }
}
