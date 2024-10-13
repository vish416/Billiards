using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTimerBarScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mask.fillAmount = 1 - gameManager.GetTurnRatio();
    }
}
