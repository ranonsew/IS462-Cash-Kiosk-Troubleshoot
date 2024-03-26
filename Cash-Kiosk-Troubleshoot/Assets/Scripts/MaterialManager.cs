using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public GameObject ballObject;
    public Material[] materials = new Material[2]; //for 3 materials
    // Start is called before the first frame update
    private int index = 0;
    void Start()
    {
        // // Set color as Random here upon clicking it will be blue as a way of fixing the lights:
        int color = Random.Range(0, 2);
        index = color;
        Debug.Log("color"+ index.ToString());
        // ballObject.GetComponent<MeshRenderer>().material = materials[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeMaterial(){
        Debug.Log("Change material triggered");
    //  if(index + 1 < materials.Length) {
    //     index += 1;
    //  }else{ index = 0; }; //Checking the material is in the list
    index = 1;
    ballObject.GetComponent<MeshRenderer>().material = materials[1];
}
}
