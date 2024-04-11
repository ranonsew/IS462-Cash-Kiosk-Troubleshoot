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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Raycast to detect if the player is touching a hole
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object hit is one of the holes
                if (hit.collider.CompareTag("hole"))
                {
                    // Spawn the string object and make it move through all the holes
                    SpawnString(hit.point);
                }
            }
        }
    }

    public void SpawnString(Vector3 startLocation)
    {
        GameObject newString = Instantiate(stringPrefab, startLocation, Quaternion.identity);
        ZiptieManager stringMovement = newString.GetComponent<ZiptieManager>();
        if (stringMovement != null)
        {
            // Pass the array of hole positions to the string movement script
            stringMovement.SetHolePositions(holePositions);
        }
        else
        {
            Debug.LogError("String prefab does not have StringMovement component!");
            Destroy(newString); // Destroy the incorrectly instantiated object
        }
    }

    public void FindHolePositions()
    {
        GameObject[] holeObjects = GameObject.FindGameObjectsWithTag("hole");
        holePositions = new Transform[holeObjects.Length];
        for (int i = 0; i < holeObjects.Length; i++)
        {
            holePositions[i] = holeObjects[i].transform;
        }
    }
}
