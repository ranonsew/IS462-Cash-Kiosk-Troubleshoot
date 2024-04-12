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
        // string scene = PointsManager.instance.previousScene;
        //float[] items = PointsManager.instance.points[0];
        // completionRate.text = "Completed: " + PointsManager.instance.fetchScores(PointsManager.instance.previousScene, "completionRate").ToString();
        // numErrors.text = "Total Errors: " + PointsManager.instance.fetchScores(PointsManager.instance.previousScene, "numErrors").ToString();
        // timeInSecs.text = "Time Taken: " + PointsManager.instance.fetchScores(PointsManager.instance.previousScene, "timeInSec").ToString();
        // overall.text = "Current Score: " + PointsManager.instance.fetchScores(PointsManager.instance.previousScene, "overall").ToString();
        //completionRate.text = "Completed: " + items[0].ToString();
        //numErrors.text = "Total Errors: " + items[1].ToString();
        //timeInSecs.text = "Time Taken: " + items[2].ToString();
        //overall.text = "Current Score: " + items[3].ToString();

        completionRate.text = "Completed: 100%";
        numErrors.text = "Total Errors: 2";
        timeInSecs.text = "Time Taken: 12s";
        overall.text = "Current Score: 3245";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
