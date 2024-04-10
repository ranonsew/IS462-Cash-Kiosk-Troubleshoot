using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Table : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }



using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
// using String;
    // public class Table : VerticalLayoutGroup {
        public class Table : MonoBehaviour {
        // public GameObject base;
        // public float timeRemaining = 10;
        // public TMP_Text messageText;
        // public bool WinnerTriggered = false;
        public TextMeshPro row;
        public Transform rowLocation;

        // public override void CalculateLayoutInputVertical()
        // {
        //     base.CalculateLayoutInputVertical();
        //     rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, minHeight);

        // }

        void Start(){

            Debug.Log("WinnieTriggered");
// var rotation = Quaternion.LookRotation(rowLocation.position);
// // rotation *= Quaternion.Euler ((float) 18.10, (float) -26, 0); // this adds a 90 degrees Y rotation
// rotation *= Quaternion.Euler (0,0,0); // this adds a 90 degrees Y rotation

        // // TextMeshPro b = Instantiate(row, rowLocation.position + new Vector3 (0, 0, 0), rotation);
        // TextMeshPro b = Instantiate(row, rowLocation.position + new Vector3 (0, -3, 0), Quaternion.Euler (0,0,0));
        // // TextMeshPro b = Instantiate(row, Transform(rowLocation.position + new Vector3 (-3, 0, 0), rowLocation.rotation), worldPositionStays:false);
        // b.transform.SetParent(rowLocation.transform, false);

        int startNum = Random.Range(50, 200);

//         string formatString = "{0,10}. {}\n";
// int value1 = 16932;
// int value2 = 15421;
// string result = String.Format(formatString, 
//                               value1, "helloooo");
        
        TextMeshPro c;

        for (int i = startNum-5; i < startNum; i++){
            Debug.Log("i:" + (i - (startNum-5)));
            
            c = Instantiate(row, rowLocation.position + new Vector3 (0, -3 * (i - (startNum-5)), 0), Quaternion.Euler (0,0,0));

            c.transform.SetParent(rowLocation.transform, false);
            c.text = "1. Ron 2000";
                
            }
        // GameObject b_object = b;
        // // b.transform.parent = this.transform;
        // GameObject(b_object).setParent(this.transform);
        // TextMeshPro b = Instantiate(row, Transform(rowLocation.position, rowLocation.rotation), worldPositionStays:false);
        // Debug.Log("location row: " + rowLocation);

        }

    }