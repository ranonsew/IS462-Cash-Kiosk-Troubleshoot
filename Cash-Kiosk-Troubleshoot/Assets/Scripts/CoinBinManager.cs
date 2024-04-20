using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoinBinManager : MonoBehaviour
{
    public static bool unlocked = false; 
    public GameObject coinPrefab;
    public Transform coinSpawnLocation;

    [SerializeField] Animator coinBinAnim;

    private void Start()
    {
        if (Random.value <= 0.1f) {
            GameObject coin = Instantiate(coinPrefab, coinSpawnLocation.position, Quaternion.identity);
        }
    }

    public void closeBin()
    {
        Debug.Log("closing");
        if (unlocked)
        {
            coinBinAnim.SetBool("isOpen", false);
        }
    }


}
