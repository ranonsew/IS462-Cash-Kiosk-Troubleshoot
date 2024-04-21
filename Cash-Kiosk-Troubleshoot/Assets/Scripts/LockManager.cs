using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LockManager : MonoBehaviour
{
    public string password = string.Empty;
    public int wrongAttempts = 0;

    public GameObject panel;

    public AudioSource audioSource;
    public AudioClip alertSound; 
    public AudioClip warningSound;
    public AudioClip unlockedSound;
    public AudioClip lockedSound;

    public TextMeshProUGUI armedText;
    public TextMeshProUGUI unarmedText;

    public CashCollectionInstructions instruction;


    public void getInput(GameObject go)
    {
        string character = go.name;
        Debug.Log(character);
        Debug.Log(password);

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
            armedText.gameObject.SetActive(true);
            unarmedText.gameObject.SetActive(false);

            clearInput();
            audioSource.clip = lockedSound;
            audioSource.Play();
            panel.SetActive(false);

        }
        else if ((character == "OFF" || character == "#") & checkInput(password))
        {
            clearInput();
            audioSource.clip = unlockedSound;
            audioSource.Play();
            panel.SetActive(false);

            if (character == "OFF")
            {
                instruction.callInstruction();
                instruction.callInstructionsAfterPassword();
                StorageManager.armActive = false;
                armedText.gameObject.SetActive(false);
                unarmedText.gameObject.SetActive(true);
            } 
            else if (character == "#") {
                StorageManager.lockActive = false;
                Debug.Log(StorageManager.lockActive);
            }
        }

        else if ((character == "ARM" || character == "OFF" || character == "#") & !checkInput(password))
        {
            clearInput();
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
        return password == "1234";
    }

    public void clearInput()
    {
        password = string.Empty;
    }
}
