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

        completionRate.text = "Completed: " + PointsManager.instance.fetchScores("SceneC", "completionRate").ToString();
        numErrors.text = "Total Errors: " + PointsManager.instance.fetchScores("SceneC", "numErrors").ToString();
        timeInSecs.text = "Time Taken: " + PointsManager.instance.fetchScores("SceneC", "timeInSec").ToString();
        overall.text = "Current Score: " + PointsManager.instance.fetchScores("SceneC", "overall").ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
