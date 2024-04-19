using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateScoreManager : MonoBehaviour
{


    public TextMeshPro completionRate;
    public TextMeshPro numErrors;
    public TextMeshPro timeInSecs;
    public TextMeshPro overall;
    // Start is called before the first frame update
    void Start()
    {
        string previousSceneSet = "SceneC";
        if  (PointsManager.instance != null || PointsManager.instance.previousScene != null){
             previousSceneSet = PointsManager.instance.previousScene;}
        completionRate.text = "Completed: " + (PointsManager.instance.fetchScores(previousSceneSet, "completionRate")*100).ToString();
        numErrors.text = "Total Errors: " + PointsManager.instance.fetchScores(previousSceneSet, "numErrors").ToString();
        timeInSecs.text = "Time Taken: " + PointsManager.instance.fetchScores(previousSceneSet, "timeInSec").ToString();
        overall.text = "Current Score: " + PointsManager.instance.fetchScores(previousSceneSet, "overall").ToString();
        

        // for individual testing without pointsmanager
        // completionRate.text = "Completed: 100%";
        // numErrors.text = "Total Errors: 2";
        // timeInSecs.text = "Time Taken: 12s";
        // overall.text = "Current Score: 3245";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
