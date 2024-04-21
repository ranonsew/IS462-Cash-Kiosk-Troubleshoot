using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KioskDoorManager : MonoBehaviour
{
    public bool isUnlocked = false;
    public bool isInUse = false;
    public bool isOpen = false;

    [SerializeField] Animator sideDoorAnim;
    [SerializeField] Animator frontDoorAnim;

    public void SetIsUnlocked(bool setIsUnlocked)
    {
        isUnlocked = setIsUnlocked;
    }

    public void SetIsInUse(bool setIsInUse)
    {
        isInUse = setIsInUse;
    }

    public void SetCoinDoor(bool toOpen)
    {
        if (toOpen)
        {
            if (isUnlocked)
            {
                sideDoorAnim.SetBool("isOpen", true);
                frontDoorAnim.SetBool("isOpen", true);
                isOpen = true;
            }
        }
        else
        {
            if (!isInUse)
            {
                sideDoorAnim.SetBool("isOpen", false);
                frontDoorAnim.SetBool("isOpen", false);
                isOpen = false;
            }
        }
    }

    public void ToggleDoor()
    {
        Debug.Log("toggle door triggered");
        if (isOpen)
        {
            if (!isInUse)
            {
                Debug.Log("close door");
                sideDoorAnim.SetBool("isOpen", false);
                frontDoorAnim.SetBool("isOpen", false);
                isOpen = false;
            }
        }
        else
        {
            if (isUnlocked)
            {
                Debug.Log("open door");
                sideDoorAnim.SetBool("isOpen", true);
                frontDoorAnim.SetBool("isOpen", true);
                isOpen = true;
            }
        }
    }
}
