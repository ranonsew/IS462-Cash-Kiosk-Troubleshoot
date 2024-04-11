using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunnyBagSocketManager : XRSocketInteractor
{
    public string[] targetTag;
    public GameObject CoinPrefab;
    public Transform coinInstantiateTransform;
    public Transform enterBagPosition;
    public Transform maxDropHeight;

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
        Vector3 coinInstantiatePosition = coinInstantiateTransform.position;

        // Start coroutine for each coin
        StartCoroutine(SpawnCoins(coinInstantiatePosition, 100));
    }

    IEnumerator SpawnCoins(Vector3 startPosition, int numCoins)
    {
        for (int i = 0; i < numCoins; i++)
        {
            // Instantiate coin prefab
            GameObject coin = Instantiate(CoinPrefab, startPosition, Quaternion.identity);
            coin.GetComponent<Rigidbody>().isKinematic = true;

            // Move coin into bag
            Vector3 direction = (enterBagPosition.position - coin.transform.position).normalized;

            // Move the coin towards the bag
            yield return StartCoroutine(MoveObject(coin, direction, maxDropHeight));

            // Delay before spawning the next coin
            yield return new WaitForSeconds(0.01f); // Adjust as needed
        }
    }

    IEnumerator MoveObject(GameObject coin, Vector3 direction, Transform maxDropHeight)
    {
        // Move the coin towards the bag
        while (Vector3.Dot(direction, enterBagPosition.position - coin.transform.position) > 0)
        {
            coin.transform.Translate(direction * 1f * Time.deltaTime);
            yield return null;
        }

        // Drop the coin straight down until a specified height
        coin.GetComponent<Rigidbody>().isKinematic = false;
        while (coin.transform.position.y > maxDropHeight.position.y)
        {
            coin.transform.Translate(Vector3.down * 1f * Time.deltaTime);
            yield return null;
        }

        // Once the coin reaches the minimum drop height, it should pile up
        coin.GetComponent<Rigidbody>().isKinematic = true;
    }

}
