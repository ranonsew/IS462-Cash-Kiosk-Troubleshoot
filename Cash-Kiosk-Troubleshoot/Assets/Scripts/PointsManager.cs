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
    // completion - percentage, number of errors, time factor, overall take into account all factors
    // sceneC{
    //     percentCompletion: 20,
    //     numErrors: 20,
    //     timeInSec: 10,
    //     overallScore: 100,
    // }
    // public static PointsManager instance;
    public float[][] points;
    // public ParticleSystem p;

    public TextMeshPro pointsDisplay;


    public string previousScene, currentScene;
    public string pointsString;

 
    #region Init / Gestion du singleton
 
    public static PointsManager instance;
    // public static PointsManager Instance(){
    //     // get
    //     // {
    //         if (instance == null)
    //         {
    //             Debug.Log("Getting new instance");
    //             instance = new PointsManager();
    //         }

    //         Debug.Log("Getting existing instance");
 
    //         return instance;
    //     // };
    // }
 
    #endregion
 
     void Awake() {
        DontDestroyOnLoad(this);
        }


    void Start(){

        // if (instance == null)
        //     instance = this;
        // else
        //     Destroy(gameObject);

        if (PointsManager.instance == null){
            instance = this;  
        }


        if (points == null){
            points = new float[][]{
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0}
        };}

        string result = ArrayToString(points);
        pointsString = result.Replace(",}", "}").Substring(1);
        // if (this.instance.previousScene == "SceneC"){
        //     pointsString = "yayyyy";
        // }


        Debug.Log("this.instance.previousScene:" + PointsManager.instance.previousScene);
         Debug.Log("this.instance.points:" + PointsManager.instance.points);

        // if (p != null){
        // var emission = p.emission; // Stores the module in a local variable
        // emission.enabled = false; // Applies the new value directly to the Particle System}
    // }
    }

// // SceneC, SceneD, SceneE, SceneF
// // completionRate, numErrors, timeInSec, overall
// updateScore(string sceneName, string metric, int metricRate)
    public float fetchScores(string sceneName, string metric){

        Debug.Log("getting score here: " + sceneName + " --- ");
        int sceneIdx = 0;
        int metricIdx = 0;
        if (sceneName == "SceneC"){
            sceneIdx = 0;
        }else if(sceneName == "SceneD"){
            sceneIdx = 1;
        }else if(sceneName == "SceneE"){
            sceneIdx = 2;
        }else{
            sceneIdx = 3;
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

        Debug.Log("hereee: "+ sceneIdx.ToString() + ", otehr one: " + metricIdx.ToString() + " points: " + points);
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
        }else{
            sceneIdx = 3;
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
        if (points[sceneIdx][0] >= 1){
            waitLoadResultsScene();
            // loadResultsScene();
            // StartConfetti();
        }

        pointsString = ArrayToString(points);
    }

    void Update()
    {}


    // public void StartConfetti()
    // {
    //     StartCoroutine(createConfetti());
    // }


        void waitLoadResultsScene(){
            StartCoroutine(loadResultsScene());
        }


    // IEnumerator createConfetti(){
    //         var emission = p.emission; // Stores the module in a local variable
    //         emission.enabled = true; // Applies the new value directly to the Particle System
    //         //Wait for 2 seconds
    //         yield return new WaitForSeconds(6);
            
    //         emission.enabled = false; // Applies the new value directly to the Particle System
    //         yield return new WaitForSeconds(2);

    // }



IEnumerator loadResultsScene(){
    yield return new WaitForSeconds(3);
    SceneManager.LoadScene("SceneResults");
}




}