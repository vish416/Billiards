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
    // Use only the first part of the string before the '$' symbol
    string[] temp = text.text.Split('$');
    string baseText = temp.Length > 0 ? temp[0] : "";  // Handle cases with no '$' character

    if (Achievements.firstPurchaseAchievement == 1)
    {
        text.text = baseText + " Completed";  // Ensure no duplication
    }
    else
    {
        text.text = baseText + " $100";  // Reset to the default text
    }
}


}
