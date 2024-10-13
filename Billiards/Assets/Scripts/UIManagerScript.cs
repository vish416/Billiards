using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManagerScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    public Text gameTimerTextbox;
    public Text turnTimerTextbox;
    public Text matchupTextbox;

    private float gameTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        matchupTextbox.text = gameManager.player1 + " vs " + gameManager.player2;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameTimer();
        UpdateMatchup();
        // Turn Timer update handled by Game Manager (using GameManager variables)
    }

    public void UpdateTurnTimer(float time)
    {
        int seconds = Mathf.FloorToInt(time);

        turnTimerTextbox.text = "Turn Timer:\n" + seconds.ToString() + "s";
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
