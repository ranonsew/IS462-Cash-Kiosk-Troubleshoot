using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunnyBagSocketManager : XRSocketInteractor
{
    public string[] targetTag;
    public GameObject CoinPrefab;
    public Transform coinSpawnTransform;
    public GameObject CoinsInGunnyPrefab;
    public Transform CoinsInGunnyTransform;
    public Transform GunnyBagTransform;

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

    public void CollectCoins()
    {
        float spawnOffset = Random.Range(-0.1f, 0.1f);
        Vector3 coinSpawnPosition = coinSpawnTransform.position + new Vector3(0f, spawnOffset, 0f);

        // Start coroutine for each coin
        StartCoroutine(SpawnCoins(coinSpawnPosition, 29));
    }

    IEnumerator SpawnCoins(Vector3 startPosition, int numCoins)
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
                animator.SetTrigger("Gunny");
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

        // Instantiate coin in gunny prefab
        GameObject spawnCoinsInGunny = Instantiate(CoinsInGunnyPrefab, CoinsInGunnyTransform.position, Quaternion.identity);

        // Set spawnCoinsInGunny as the parent of the childObject
        spawnCoinsInGunny.transform.SetParent(GunnyBagTransform);
    }

}
