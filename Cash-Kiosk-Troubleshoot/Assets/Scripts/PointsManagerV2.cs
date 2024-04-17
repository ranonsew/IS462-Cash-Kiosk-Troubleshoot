using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Per Scene Instances version for the PointsManager
/// </summary>
public class PointsManagerV2 : MonoBehaviour
{
    public string sceneName; // input in inspector
    [HideInInspector] public int completionRate = 0;
    [HideInInspector] public int numErrors = 0;
    [HideInInspector] public int timeInSeconds = 0;
    public SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Check results, and if good, load scene results
    /// </summary>
    public void CheckResults()
    {
        // check stuff
        if (false)
        {
            sceneLoader.LoadScene("SceneResults");
        }
    }
}
