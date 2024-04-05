using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColour : MonoBehaviour
{
    public GameObject coinLight;
    public GameObject noteLight;

    public Material[] materials = new Material[2];

    public void blueToYellow(bool coinChange, bool noteChange)
    {
        if (coinChange) 
        {
            coinLight.GetComponent<MeshRenderer>().material = materials[1];
        }

        if (noteChange)
        {
            noteLight.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    public void yellowToBlue(bool coinChange, bool noteChange)
    {
        if (coinChange)
        {
            coinLight.GetComponent<MeshRenderer>().material = materials[0];
        }

        if (noteChange)
        {
            noteLight.GetComponent<MeshRenderer>().material = materials[0];
        }
    }
}
