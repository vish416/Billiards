using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstLossAchievement : MonoBehaviour
{
    private Text text;
    private Achievements achievements;
    private int previousFirstLossState = -1;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        achievements = GetComponentInParent<Achievements>();
        UpdateFirstLossText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Achievements.firstLossAchievement != previousFirstLossState)
        {
            UpdateFirstLossText();
            previousFirstLossState = Achievements.firstLossAchievement;  // Update the tracked state
        }
    }

void UpdateFirstLossText()
    {
        string[] temp = text.text.Split('$');
        string baseText = temp.Length > 0 ? temp[0] : "";  // Handle cases with no '$' character

        if (Achievements.firstLossAchievement == 1)
        {
            text.text = baseText + " Completed";
        }
        else
        {
            text.text = baseText + " $100";
        }
    }
}