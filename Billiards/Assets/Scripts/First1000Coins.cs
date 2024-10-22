using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class First1000Coins : MonoBehaviour
{
    private Text text;
    private Achievements achievements;
    private int previousFirst1000State = -1;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        achievements = GetComponentInParent<Achievements>();
        UpdateFirst1000Text();
    }

    // Update is called once per frame
    void Update()
    {
        if (Achievements.first1000Achievement != previousFirst1000State)
        {
            UpdateFirst1000Text();
            previousFirst1000State = Achievements.first1000Achievement;  // Update the tracked state
        }
    }

    void UpdateFirst1000Text()
    {
        string[] temp = text.text.Split('$');
    string baseText = temp.Length > 0 ? temp[0] : "";  // Handle cases with no '$' character

    if (Achievements.first1000Achievement == 1)
    {
        text.text = baseText + " Completed";  // Ensure no duplication
    }
    else
    {
        text.text = baseText + " $100";  // Reset to the default text
    }
    }
}
