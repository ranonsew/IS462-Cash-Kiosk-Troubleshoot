using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class LineToTransform : MonoBehaviour {
    public Transform ConnectTo;
    LineRenderer line;

    void Start() {
        
        
    }
    void LateUpdate() {
        UpdateLine();
    }

    public void UpdateLine() {

        // Assign Line if first attempt
        if (line == null) {
            line = GetComponent<LineRenderer>();
            if (line != null) {
                line.useWorldSpace = false;
            }
        }

        if(line != null) {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, transform.InverseTransformPoint(ConnectTo.position));
        }
    }

    void OnDrawGizmos() {
        UpdateLine();
    }
}