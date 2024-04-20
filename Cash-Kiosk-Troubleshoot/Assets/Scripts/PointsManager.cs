using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour
{
    public float[][] points;

    public TextMeshPro pointsDisplay;


    public string previousScene, currentScene;
    public string pointsString;

 
    #region Init / Gestion du singleton
 
    public static PointsManager instance;
 
    #endregion
 
     void Awake() {
        DontDestroyOnLoad(this);
        }


    void Start(){

        if (PointsManager.instance == null){
            instance = this;
        }


        if (points == null){
            points = new float[][]{
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0}
        };}

        string result = ArrayToString(points);
        pointsString = result.Replace(",}", "}").Substring(1);}

    public float fetchScores(string sceneName, string metric){

        // Debug.Log("getting score here: " + sceneName + " --- ");
        int sceneIdx = 0;
        int metricIdx = 0;
        if (sceneName == "SceneC"){
            sceneIdx = 0;
        }else if(sceneName == "SceneD"){
            sceneIdx = 1;
        }else if(sceneName == "SceneE"){
            sceneIdx = 2;
        }else if(sceneName == "SceneF"){
            sceneIdx = 3;
        }else if(sceneName == "CashCollection"){
            sceneIdx = 4;
        }
        if (metric == "completionRate"){
            metricIdx = 0;
        }else if(metric == "numErrors"){
            metricIdx = 1;
        }else if(metric == "timeInSec"){
            metricIdx = 2;
        }else{
            metricIdx = 3;

            return (points[sceneIdx][0] * 500 + 500 / (points[sceneIdx][2]/1000)  - 500 * (points[sceneIdx][1])) / 10;
        }

        // Debug.Log("hereee: "+ sceneIdx.ToString() + ", otehr one: " + metricIdx.ToString() + " points: " + points);
        return points[sceneIdx][metricIdx];
    }



    public string ArrayToString(float[][] points){
            var result = string.Empty;
    var maxI = points.GetLength(0);
    var maxJ = points[0].GetLength(0);
    for (var i = 0; i < maxI; i++)
    {
        result += ",{";
        for (var j = 0; j < maxJ; j++)
        {
            result += $"{points[i][j]},";
        }

        result += "}";
    }
    return result;
    }



    public void updateScore(string sceneName, string metric, float metricRate){
        previousScene = sceneName;
        int sceneIdx = 0;
        int metricIdx = 0;
        if (sceneName == "SceneC"){
            sceneIdx = 0;
        }else if(sceneName == "SceneD"){
            sceneIdx = 1;
        }else if(sceneName == "SceneE"){
            sceneIdx = 2;
        }else if(sceneName == "SceneF"){
            sceneIdx = 3;
        }else if(sceneName == "CashCollection"){
            sceneIdx = 4;
        }
        if (metric == "completionRate"){
            metricIdx = 0;
        }else if(metric == "numErrors"){
            metricIdx = 1;
        }else if(metric == "timeInSec"){
            metricIdx = 2;
        }else{
            metricIdx = 3;
        }

        points[sceneIdx][metricIdx] += metricRate;
        // shower confetti if completion done:
        // if (points[sceneIdx][0] >= 1){
        //     waitLoadResultsScene();
        // }

        pointsString = ArrayToString(points);
    }

    void Update()
    {}

//         public void waitLoadResultsScene(){
//             StartCoroutine(loadResultsScene());
//         }

// IEnumerator loadResultsScene(){
//     yield return new WaitForSeconds(6);
//     // SceneManager.LoadScene("SceneResults");
// }




}