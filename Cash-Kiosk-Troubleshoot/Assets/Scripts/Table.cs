// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

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
using System.Collections.Generic;
using TMPro;
        public class Table : MonoBehaviour {
        public TextMeshPro row;
        public Transform rowLocation;
        public string[] names = new string[]{ "Shane", "Jane", "Jake", "Joseph", "Olivia", "Ron", "John", "Simon", "Minnie", "Mickey", "Warren"};
        void Start(){
        int startNum = Random.Range(50, 200);
        TextMeshPro c;
        row.text = "";
        int mainPlayerScore = 3000;
        if (PointsManager.instance != null){
            mainPlayerScore = (int) PointsManager.instance.fetchScores("SceneC", "overall");
        }
        int newPlayerScore = 0;

        List<int> list = new List<int>();
        list.Add(mainPlayerScore);
        for (int i = 0; i < 5; i++){
                list.Add(Random.Range(0, mainPlayerScore - 1));
                list.Add(Random.Range( mainPlayerScore + 1, mainPlayerScore + 2000));
        }
        list.Sort((a, b) => b.CompareTo(a)); // descending sort

        for (int i = startNum-5; i < startNum+5; i++){
            c = Instantiate(row, rowLocation.position + new Vector3 (4, -3 * (i - (startNum-5)), -4), Quaternion.Euler (0,0,0));
            c.transform.SetParent(rowLocation.transform, false);
            c.text = i.ToString()+". "+ names[i - (startNum-5)] + " " + list[i - (startNum-5)].ToString();

            string htmlValue = "#F68918";
            if (i == startNum) htmlValue = "#1CDF41";
            Color newCol;
            if (ColorUtility.TryParseHtmlString(htmlValue, out newCol)){ c.color = newCol; }
            }
        }

    }