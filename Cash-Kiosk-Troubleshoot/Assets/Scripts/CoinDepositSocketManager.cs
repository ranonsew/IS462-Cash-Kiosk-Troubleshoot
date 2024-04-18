using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoinDepositManager : XRSocketInteractor
{
    public string[] targetTag;
    public GameObject CoinPrefab;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    private bool MatchUsingTag(IXRInteractable interactable)
    {
        XRBaseInteractable XRBaseInteractable = interactable as XRBaseInteractable;
        if (XRBaseInteractable == null) return false;

        bool tagMatch = false;
        foreach (string tag in targetTag)
        {
            tagMatch = tagMatch || XRBaseInteractable.CompareTag(tag);
        }
        return tagMatch;
    }

    public void DestroyPlacedObject(SelectEnterEventArgs args)
    {
        XRBaseInteractable XRBaseInteractable = args.interactableObject as XRBaseInteractable;
        Destroy(XRBaseInteractable.gameObject);
    }

    public void DepositCoins(SelectEnterEventArgs args)
    {
        Vector3 coinSpawnPosition = new Vector3(-2.3738f, 1.9041f, 2.6045f);

        // Trigger animation to tilt the coin bag
        XRBaseInteractable XRBaseInteractable = args.interactableObject as XRBaseInteractable;
        GameObject coin_bag = XRBaseInteractable.gameObject;
        Animator coinBagAnimator = coin_bag.GetComponent<Animator>();
        XRSocketInteractor socketInteractor = args.interactorObject as XRSocketInteractor;
        socketInteractor.enabled = false;
        if (coinBagAnimator != null)
        {
            Debug.Log("coin bag animation triggered");
            // Play the animation
            coinBagAnimator.SetTrigger("pour_coins");
        }
        else
        {
            Debug.LogError("Animator component not found on the coinBag prefab!");
        }

        // Wait for 1 second
        StartCoroutine(WaitAndSpawnCoins(coinSpawnPosition, 29, coin_bag));
    }

    IEnumerator WaitAndSpawnCoins(Vector3 startPosition, int numCoins, GameObject coin_bag)
    {
        // Wait for 1 second
        yield return new WaitForSeconds(0.49f);

        // Start coroutine for each coin
        StartCoroutine(SpawnCoins(startPosition, numCoins, coin_bag));
    }

    IEnumerator SpawnCoins(Vector3 startPosition, int numCoins, GameObject coin_bag)
    {
        for (int i = 0; i < numCoins; i++)
        {
            // Instantiate coin prefab
            GameObject spawnedCoin = Instantiate(CoinPrefab, startPosition, Quaternion.identity);
            spawnedCoin.GetComponent<Rigidbody>().isKinematic = true;

            // Get the Animator component of the spawned coin
            Animator animator = spawnedCoin.GetComponent<Animator>();

            if (animator != null)
            {
                // Play the animation
                animator.SetTrigger("Float_Replenishment");
            }
            else
            {
                Debug.LogError("Animator component not found on the coin prefab!");
            }

            // Destroy the coin after 0.1 seconds
            Destroy(spawnedCoin, 0.4f);

            // Delay before spawning the next coin
            yield return new WaitForSeconds(0.01f); // Adjust as needed
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(coin_bag);
    }

    public void ActivateCoinSocket()
    {
        gameObject.SetActive(true);
    }
}
