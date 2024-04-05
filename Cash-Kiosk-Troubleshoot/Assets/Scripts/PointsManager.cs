using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;

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

    // public TextMeshPro pointsDisplay;

    void Start(){

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // [scene][percentCompletion, numErrors, timeInSec, overallScore]

        points = new float[][]{
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0},
            new float[]{0, 0, 0, 0}
        };
    }

    public void updateScoreCompletion(string sceneName, int completionRate){
        int sceneIdx = 0;
        if (sceneName == "SceneC"){
            sceneIdx = 0;
        }else if(sceneName == "SceneD"){
            sceneIdx = 1;
        }else if(sceneName == "SceneE"){
            sceneIdx = 2;
        }else{
            sceneIdx = 3;
        }

        points[sceneIdx][0] = completionRate;
    }

    void Update()
    {}






}