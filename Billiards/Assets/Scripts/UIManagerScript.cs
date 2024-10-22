using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManagerScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    public Text gameTimerTextbox;
    public Text matchupTextbox;

    private float gameTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        matchupTextbox.text = gameManager.player1 + " vs " + gameManager.player2;
    }

    //for unit testing
    public void TestStart()
    {
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        { 
            UpdateGameTimer();
            UpdateMatchup();
        }
    }

    //for unit testing
    public void TestUpdate()
    {
        Update();
    }


    private void UpdateGameTimer()
    {
        gameTimer += Time.deltaTime;

        int minutes = (int)(gameTimer / 60);
        int seconds = (int)(gameTimer % 60);

        gameTimerTextbox.text = $"{minutes}:{seconds:D2}";
    }

    private void UpdateMatchup()
    {
        if (gameManager.currentPlayer == gameManager.player1)
        {
            matchupTextbox.text = $"<b>{gameManager.player1}</b> vs {gameManager.player2}";
        }
        else
        {
            matchupTextbox.text = $"{gameManager.player1} vs <b>{gameManager.player2}</b>";
        }
    }
}
