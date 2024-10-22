using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstOwn3Skins : MonoBehaviour
{
    private Text text;
    private Achievements achievements;
    private int previousFirst3SkinsState = -1;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        achievements = GetComponentInParent<Achievements>();
        UpdateFirst3SkinsText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Achievements.first3SkinsAchievement != previousFirst3SkinsState)
        {
            UpdateFirst3SkinsText();
            previousFirst3SkinsState = Achievements.first3SkinsAchievement;  // Update the tracked state
        }
    }

    void UpdateFirst3SkinsText()
    {
        string[] temp = text.text.Split('$');
        string baseText = temp.Length > 0 ? temp[0] : "";  // Handle cases with no '$' character

    if (Achievements.first3SkinsAchievement == 1)
    {
        text.text = baseText + " Completed";  // Ensure no duplication
    }
    else
    {
        text.text = baseText + " $100";  // Reset to the default text
    }
    }
}
