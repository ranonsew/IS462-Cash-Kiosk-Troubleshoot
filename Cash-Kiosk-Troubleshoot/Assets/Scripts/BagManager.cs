using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public GameObject stringPrefab;
    public Transform[] holePositions;

    void Start()
    {
        FindHolePositions();
    }

    void Update()
    {
        
    }

    public void FindHolePositions()
    {
        GameObject[] holeObjects = GameObject.FindGameObjectsWithTag("hole");
        holePositions = new Transform[holeObjects.Length];
        for (int i = 0; i < holeObjects.Length; i++)
        {
            holePositions[i] = holeObjects[i].transform;
        }

        Debug.Log(holePositions.Length);
    }
}
