using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCue : MonoBehaviour
{
    private Text text;
    private Purchase purchaseScript;
    private int previousCuePurchaseState = -1;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        purchaseScript = GetComponentInParent<Purchase>();
        UpdateCuePurchaseText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Purchase.cuePurchase != previousCuePurchaseState)
        {
            UpdateCuePurchaseText();
            previousCuePurchaseState = Purchase.cuePurchase;  // Update the tracked state
        }
    }

    void UpdateCuePurchaseText()
    {
        string[] temp = text.text.Split('$');
        if (Purchase.cuePurchase == 1)
        {
            text.text = temp[0] + "Purchased";
        }
        else
        {
            text.text = temp[0] + "$500";
        }
    }
}
