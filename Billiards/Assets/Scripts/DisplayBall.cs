using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBall : MonoBehaviour
{
    private Text text;
    private Purchase purchaseScript;
    private int previousEyePurchaseState = -1;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        purchaseScript = GetComponentInParent<Purchase>();
        UpdateEyePurchaseText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Purchase.eyePurchase != previousEyePurchaseState)
        {
            UpdateEyePurchaseText();
            previousEyePurchaseState = Purchase.eyePurchase;  // Update the tracked state
        }
    }

    void UpdateEyePurchaseText()
{
    if (Purchase.eyePurchase == 1)
    {
        if (!text.text.Contains("Purchased")) // Check if "Purchased" is not already in the text
        {
            text.text = text.text.Split('$')[0] + "Purchased"; // Only update if not already purchased
        }
    }
    else
    {
        text.text = text.text.Split('$')[0] + "$500"; // Reset to default price if not purchased
    }
}

}
