using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LockManager : MonoBehaviour
{
    public string password = string.Empty;
    public int wrongAttempts = 0;

    public AudioSource audioSource;
    public AudioClip alertSound; 
    public AudioClip warningSound;
    public AudioClip unlockedSound;
    public AudioClip lockedSound;

    public void getInput(GameObject go)
    {
        string character = go.name;
        Debug.Log(character);

        if (int.TryParse(character, out int temp))
        {
            password += character;
        }
        else if (character == "CLEAR")
        {
            clearInput();
        }

        // check with winnie on how this is armed/disarmed
        else if (character == "ARM" & checkInput(password))
        {
            StorageManager.armActive = true;
            clearInput();
            audioSource.clip = lockedSound;
            audioSource.Play();
        }
        else if ((character == "OFF" || character == "#") & checkInput(password))
        {
            StorageManager.armActive = false;
            clearInput();
            audioSource.clip = unlockedSound;
            audioSource.Play();
        }

        else if ((character == "ARM" || character == "OFF" || character == "#") & !checkInput(password))
        {
            wrongAttempts++;

            if (wrongAttempts < 3)
            {
                audioSource.clip = warningSound;
            } 
            else
            {
                audioSource.clip = alertSound;
            }

            audioSource.Play();
        }
    }

    public bool checkInput(string password)
    {
        return password == "0000";
    }

    public void clearInput()
    {
        password = string.Empty;
    }
}
