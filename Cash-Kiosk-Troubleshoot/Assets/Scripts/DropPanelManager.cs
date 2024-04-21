using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPanelManager : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    public static bool isOpen = false;

    public AudioSource audioSource;
    public AudioClip clickSound;

    public void ToggleDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            doorAnim.SetBool("isOpen", true);
        } else
        {
            isOpen = false;
            doorAnim.SetBool("isOpen", false);
            StartCoroutine(PlaySoundWithDelay());
        }
    }

    IEnumerator PlaySoundWithDelay()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second

        // Set the audio clip
        audioSource.clip = clickSound;
        // Play the audio clip
        audioSource.Play();
    }
}
