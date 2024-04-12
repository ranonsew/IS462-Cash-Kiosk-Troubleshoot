using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ReceiptManager : MonoBehaviour
{
    public GameObject receiptPrefab;
    public Transform receiptDispenserPosition;


    public void InventoryReceipt()
    {
        string content = @"View Inventory
Notes
DENO     Pieces    Amount
$   2   :   11           $  22
$   5   :    8           $  40
$  10   :    5           $  50
$  50   :    1           $  50

Coins
DENO     Pieces    Amount
$ 0.05  :    2           $ 0.10
$ 0.10  :    8           $ 0.80
$ 0.20  :    4           $ 0.80
$ 0.50  :    8           $ 4.00
$ 1.00  :    7           $ 7.00";

        printReceipt(content);
    }

    public void collectionReceipt(string collectionType)
    {
        if (collectionType == "Coins")
        {
            string content = @"Collection Details
Coins
DENO     Pieces    Amount
$ 0.05  :    2           $ 0.10
$ 0.10  :    8           $ 0.80
$ 0.20  :    4           $ 0.80
$ 0.50  :    8           $ 4.00
$ 1.00  :    7           $ 7.00
------------------------------------
TOTAL       29         $ 12.70
------------------------------------";

            printReceipt(content);
        }
        else if (collectionType == "Notes")
        {
            string content = @"Collection Details
Notes
DENO     Pieces    Amount
$   2   :   11           $  22
$   5   :    8           $  40
$  10   :    5           $  50
$  50   :    1           $  50
------------------------------------
TOTAL       25         $ 162
------------------------------------";

            printReceipt(content);
        }
        else if (collectionType == "Unlock Coin")
        {
            string content = @"Container Removed

Total Value  :$0.00";

            printReceipt(content);
        }
    }

    // function to print receipt
    private void printReceipt(string receiptContent)
    {
        // instantiate recipt prefab and copy the text onto the receipt
        GameObject receipt = Instantiate(receiptPrefab, receiptDispenserPosition.position, Quaternion.identity);

        // Disable Rigidbody initially so that it doesn't fall
        Rigidbody rigidbody = receipt.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }

        // Find the TextMeshPro component on the canvas
        TextMeshProUGUI textMeshPro = receipt.GetComponentInChildren<TextMeshProUGUI>();

        if (textMeshPro != null)
        {
            // Set the text
            textMeshPro.text = receiptContent;
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not found on the instantiated prefab.");
        }

        // print receipt out from dispenser
        receipt.transform.Translate(Vector3.right * 0.5f);
    }

    public void EnableRigidBody(SelectExitEventArgs args)
    {
        GameObject receipt = (args.interactableObject as XRBaseInteractable).gameObject;

        Rigidbody rigidbody = receipt.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
        }
    }
}
