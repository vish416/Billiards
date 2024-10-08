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
        string[] temp = text.text.Split('$');
        if (Purchase.eyePurchase == 1)
        {
            text.text = temp[0] + "Purchased";
        }
        else
        {
            text.text = temp[0] + "$500";
        }
    }
}
