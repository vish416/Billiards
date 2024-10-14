using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPurchase : MonoBehaviour
{
    private Text text;
    private Purchase purchaseScript;
    private int previousTablePurchaseState = -1;  // Track previous purchase state

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        purchaseScript = GetComponentInParent<Purchase>();
        UpdateTablePurchaseText();  // Call once to initialize the text correctly
    }

    // Update is called once per frame
    void Update()
    {
        // Only update the text if the purchase state has changed
        if (Purchase.tablePurchase != previousTablePurchaseState)
        {
            UpdateTablePurchaseText();
            previousTablePurchaseState = Purchase.tablePurchase;  // Update the tracked state
        }
    }

    // A separate method to handle text updates
    void UpdateTablePurchaseText()
    {
        string[] temp = text.text.Split('$');
        if (Purchase.tablePurchase == 1)
        {
            text.text = temp[0] + "Purchased";
        }
        else
        {
            text.text = temp[0] + "$500";
        }
    }
}
