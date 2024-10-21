using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstWinAchieved : MonoBehaviour
{
    private Text text;
    private int previousFirstWinState = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Achievements.firstWinAchievement != previousFirstWinState)
        {
            UpdateFirstWinText();
            previousFirstWinState = Achievements.firstWinAchievement;  // Update the tracked state
        }
    }

    void UpdateFirstWinText()
    {
        string[] temp = text.text.Split('$');
        string baseText = temp.Length > 0 ? temp[0] : "";  // Handle cases with no '$' character

        if (Achievements.firstWinAchievement == 1)
        {
            text.text = baseText + "Completed";
        }
        else
        {
            text.text = baseText + "$100";
        }
    }
}
