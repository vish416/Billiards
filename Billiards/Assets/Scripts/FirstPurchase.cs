using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPurchase : MonoBehaviour
{
    private Text text;
    private Achievements achievements;
    private int previousFirstPurchaseState = -1;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        achievements = GetComponentInParent<Achievements>();
        UpdateFirstPurchaseText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Achievements.firstPurchaseAchievement != previousFirstPurchaseState)
        {
            UpdateFirstPurchaseText();
            previousFirstPurchaseState = Achievements.firstPurchaseAchievement;  // Update the tracked state
        }
    }

    void UpdateFirstPurchaseText()
    {
        string[] temp = text.text.Split('$');
        if (Achievements.firstPurchaseAchievement == 1)
        {
            text.text = temp[0] + "Completed";
        }
        else
        {
            text.text = temp[0] + "$100";
        }
    }
}
