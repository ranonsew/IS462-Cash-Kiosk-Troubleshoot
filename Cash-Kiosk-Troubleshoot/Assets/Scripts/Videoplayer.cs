// Examples of Video Player function

using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System;
using System.IO;
using UnityEditor;
using System.Collections.Generic;



[Serializable]
public class Media{
    public string directory;
public List<String> files;
}


public class Videoplayer : MonoBehaviour
{


        Media getFiles(){
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://127.0.0.1:45713/directory"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Media info = JsonUtility.FromJson<Media>(jsonResponse);
        // Debug.Log("this is the weather info: " + info.files);
        Debug.Log("Upload ended ");
        return info;

    }
    void Start()
    {
        // Will attach a Video Player to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        // var button = camera.AddComponent<UnityEngine>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, Video Players added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        // This will cause our Scene to be visible through the video being played.
        // videoPlayer.targetCameraAlpha = 0.5F;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        Media files = getFiles();
        // videoPlayer.url = "Assets/5 Minutes of Funny Cats and Dogs 🐱🐕🐈 Funny Videos.mp4";
        videoPlayer.url = files.directory + "/" + files.files[0];

        // Skip the first 100 frames.
        videoPlayer.frame = 100;

        // Restart from beginning when done.
        videoPlayer.isLooping = true;

        // Each time we reach the end, we slow down the playback by a factor of 10.
        videoPlayer.loopPointReached += EndReached;

        // Start playback. This means the Video Player may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        videoPlayer.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }
}