using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseManager : MonoBehaviour
{
  float _interval = 3f;
 
  float _time;
    public GameObject gameObject;
    public Light li;
    // public Material material;
    // Start is called before the first frame update
    void Start()
    {
        li = GetComponent<Light>();
    }

// Update is called once per frame
void Update () {

    _time += Time.deltaTime;
    if (_time >= _interval) {
        
      float random = Random.Range(-10f, 10f);
	// float intensity = Mathf.PerlinNoise(random, Time.time);
	// gameObject.GetComponent<Renderer>().material.SetFloat("_EmissiveIntensity", random);
    // li.intensity = random;
    // // material.SetFloat("_EmissiveIntensity", random);
    Debug.Log("Running" + random.ToString());

     float emissiveIntensity = 10;
        Color emissiveColor = Color.green;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissiveColor", emissiveColor * emissiveIntensity);


      _time = 0;
    }
	
}
}
