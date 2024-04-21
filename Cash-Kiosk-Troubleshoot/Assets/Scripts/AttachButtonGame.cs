using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttachButtonGame : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject gamePanel;
    // public GameObject[] buttons;
    Dictionary<string, bool> buttons = new Dictionary<string, bool>();
    public GameObject goodJobSign;
    public Transform goodJobSignLocation;

    public GameObject openBag;
    public GameObject closeBag;

    private GameObject activeBag;
    private GameObject nonactiveBag;
    
    public Transform newBagLocation;
    public bool gameOver;

    public TextMeshProUGUI displayInstructions;
    public GameObject completedPanel;
    public TextMeshProUGUI completedPanelText;


    // Start is called before the first frame update
    void Awake()
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
        if (openBag.activeSelf)
        {
            activeBag = openBag;
            nonactiveBag = closeBag;
            displayInstructions.text = "Make all the circles green to zip the bag";
            completedPanelText.text = "Good job, you have zipped the bag sucessfully";
        }
        else
        {
            activeBag = closeBag;
            nonactiveBag = openBag;
            displayInstructions.text = "Make all the circles green to unzip the bag";
            completedPanelText.text = "Good job, you have unzipped the bag sucessfully";
        }

        if (gameOver == false){
        if (buttons["bagbutton1"] & buttons["bagbutton2"] & buttons["bagbutton3"] & buttons["bagbutton4"] & 
        buttons["bagbutton5"] & buttons["bagbutton6"] & buttons["bagbutton7"] & buttons["bagbutton8"]){
            // GameObject b = Instantiate(goodJobSign, goodJobSignLocation.position, goodJobSignLocation.rotation);
            
            // wait();
            // Destroy(b);
            // gamePanel.SetActive(false);
            // newBagLocation newBagLocation = oldBag.transform;
            //if (gameOver == false){ 
            disableGame();
            //    gameOver = true;
            //    }
           
}
        }
    }

    public void disableGame(){
        // DestroyImmediate(oldBag, true);
        // oldBag.SetActive(false);
        activeBag.SetActive(false);
        nonactiveBag.SetActive(true);

        List<string> buttonsToModify = new List<string>();

        // Add the keys of all buttons to the list
        foreach (var kvp in buttons)
        {
            buttonsToModify.Add(kvp.Key);
        }

        // Reset button sprites to sprites[0]
        foreach (string buttonName in buttonsToModify)
        {
            Button mybutton = GameObject.Find(buttonName).GetComponent<Button>();
            mybutton.image.sprite = sprites[0];
            buttons[buttonName] = false;
        }

        //gameOver = true;
        gamePanel.SetActive(false);
        completedPanel.SetActive(true);

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
