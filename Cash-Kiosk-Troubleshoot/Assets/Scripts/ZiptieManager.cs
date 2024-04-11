using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiptieManager : MonoBehaviour
{
    public Transform[] holePositions;
    public int currentHole = 0;

    public void SetHolePositions(Transform[] positions)
    {
        holePositions = positions;
    }

    void Start()
    {
        // Ensure holePositions array is initialized
        if (holePositions == null || holePositions.Length == 0)
        {
            Debug.LogError("Hole positions are not set!");
            Destroy(gameObject); // Destroy the string object if hole positions are not set
        }
    }

    void Update()
    {
        // Move towards the next hole position
        if (currentHole < holePositions.Length)
        {
            Vector3 targetPosition = holePositions[currentHole].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2f); // Adjust speed as needed

            // Check if the string has reached the current hole position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                currentHole++; // Move to the next hole position
            }
        }
        else
        {
            // Reset to the first hole position to create a continuous loop
            currentHole = 0;
        }
    }
}
