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
        public class Table : MonoBehaviour {
        public TextMeshPro row;
        public Transform rowLocation;


        void Start(){
        int startNum = Random.Range(50, 200);
        TextMeshPro c;
        for (int i = startNum-5; i < startNum; i++){
            // Debug.Log("i:" + (i - (startNum-5)));
            c = Instantiate(row, rowLocation.position + new Vector3 (0, -3 * (i - (startNum-5)), 0), Quaternion.Euler (0,0,0));
            c.transform.SetParent(rowLocation.transform, false);
            c.text = "1. Ron 2000";
            }
//         string formatString = "{0,10}. {}\n";
// int value1 = 16932;
// int value2 = 15421;
// string result = String.Format(formatString, 
//                               value1, "helloooo");
        }

    }