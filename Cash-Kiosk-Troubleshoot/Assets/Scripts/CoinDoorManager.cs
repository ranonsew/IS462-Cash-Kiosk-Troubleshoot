using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDoorManager : MonoBehaviour
{
    public bool isUnlocked = false;
    public bool isInUse = false;
    public bool isOpen = false;

    [SerializeField]
    Animator doorAnim;

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
                doorAnim.SetBool("isOpen", true);
                isOpen = true;
            }
        }
        else
        {
            if (!isInUse)
            {
                doorAnim.SetBool("isOpen", false);
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
                doorAnim.SetBool("isOpen", false);
                isOpen = false;
            }
        }
        else
        {
            if (isUnlocked)
            {
                Debug.Log("open door");
                doorAnim.SetBool("isOpen", true);
                isOpen = true;
            }
        }
    }
}
