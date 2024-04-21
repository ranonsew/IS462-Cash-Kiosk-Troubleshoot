using Mono.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesDoorAnimation : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    public static bool isOpen = false;

    public void ToggleDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            doorAnim.SetBool("isOpen", true);
        }
        else
        {
            isOpen = false;
            doorAnim.SetBool("isOpen", false);
        }
    }
}
