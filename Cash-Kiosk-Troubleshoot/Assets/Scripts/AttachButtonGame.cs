using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttachButtonGame : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject gamePanel;
    // public GameObject[] buttons;
    Dictionary<string, bool> buttons = new Dictionary<string, bool>();
    public GameObject goodJobSign;
    public Transform goodJobSignLocation;

    public GameObject oldBag;
    public GameObject newBag;
    public Transform newBagLocation;
    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        // var sprite = Resources.Load<Sprite>("Dan 1");
        // Button.image.sprite = sprites[0];
        buttons.Add("bagbutton1", false);
        buttons.Add("bagbutton2", false);
        buttons.Add("bagbutton3", false);
        buttons.Add("bagbutton4", false);
        buttons.Add("bagbutton5", false);
        buttons.Add("bagbutton6", false);
        buttons.Add("bagbutton7", false);
        buttons.Add("bagbutton8", false);
        
    }


    void wait()
    {
        // start the coroutine
        StartCoroutine(DoSomething(10));
    }
 
    IEnumerator DoSomething(float duration)
    {
        // do something before
        Debug.Log("Before");
 
        // waits here
        yield return new WaitForSeconds(duration);
 
        // do something after
        Debug.Log("After");
    }


    // Update is called once per frame
    void Update()
    {

        if (gameOver == false){
        if (buttons["bagbutton1"] & buttons["bagbutton2"] & buttons["bagbutton3"] & buttons["bagbutton4"] & 
        buttons["bagbutton5"] & buttons["bagbutton6"] & buttons["bagbutton7"] & buttons["bagbutton8"]){
            GameObject b = Instantiate(goodJobSign, goodJobSignLocation.position, goodJobSignLocation.rotation);
            
            // wait();
            // Destroy(b);
            // gamePanel.SetActive(false);
            // newBagLocation newBagLocation = oldBag.transform;
            if (gameOver == false){ 
                disableGame();
                gameOver = true;
                }
           
}
        }
    }

    public void disableGame(){
                    Destroy(oldBag);
            GameObject newbagcreated = Instantiate(newBag, newBagLocation.position, newBagLocation.rotation);
        buttons["bagbutton1"] = false;
        buttons["bagbutton2"] = false;
        buttons["bagbutton3"] = false;
        buttons["bagbutton4"] = false;
        buttons["bagbutton5"] = false;
        buttons["bagbutton6"] = false;
        buttons["bagbutton7"] = false;
        buttons["bagbutton8"] = false;
        gameOver = true;
    }

    public void turnGreen(string buttonName){
        // buttons[i].GetComponent<Image>().sprite = sprites[1];
        Button mybutton = GameObject.Find(buttonName).GetComponent<Button>();
        if (buttons[buttonName] == false){
            mybutton.image.sprite = sprites[1];
        }else{mybutton.image.sprite = sprites[0];}
        buttons[buttonName] = !buttons[buttonName];
        
    }



}
