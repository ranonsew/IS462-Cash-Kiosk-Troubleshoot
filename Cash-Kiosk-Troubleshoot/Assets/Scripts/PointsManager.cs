using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
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
    public static PointsManager instance;
    public float[][] points;
    public ParticleSystem p;

    // public TextMeshPro pointsDisplay;

    void Start(){

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        points = new float[][]{
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0}
        };

        var emission = p.emission; // Stores the module in a local variable
        emission.enabled = false; // Applies the new value directly to the Particle System
    }

// // SceneC, SceneD, SceneE, SceneF
// // completionRate, numErrors, timeInSec, overall
// updateScore(string sceneName, string metric, int metricRate)
    public void updateScore(string sceneName, string metric, int metricRate){
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

        points[sceneIdx][metricIdx] = metricRate;
        // shower confetti if completion done:
        if (points[sceneIdx][0] >= 1){
            StartConfetti();
        }
    }

    void Update()
    {}


    public void StartConfetti()
    {
        StartCoroutine(createConfetti());
    }


    IEnumerator createConfetti(){
            var emission = p.emission; // Stores the module in a local variable
            emission.enabled = true; // Applies the new value directly to the Particle System
            //Wait for 2 seconds
            yield return new WaitForSeconds(6);
            
            emission.enabled = false; // Applies the new value directly to the Particle System
            yield return new WaitForSeconds(2);
            loadResultsScene();
    }



public void loadResultsScene(){
    SceneManager.LoadScene("SceneResults");
}




}