using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public string player1 = "Player 1";
    public string player2 = "Player 2";

    public string currentPlayer;

    public PlayerController playerController;

    public GameObject cueBall;
    public GameObject eightBall;
    public List<GameObject> stripedBalls;
    public List<GameObject> solidBalls;
    public Text showCurrentPlayer;
    public Text showMovementStatus;
    public Text gameTimer;

    private const float turnTimer = 30.0f;
    private float timeSinceTurn = 0.0f;
    private bool isPlayerTurnActive = false;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = player1;
        StartTurn();


        Debug.Log(currentPlayer);
        Debug.Log("starting game");
    }

    // Update is called once per frame
    void Update()
    {
        showMovementStatus.text = AnyBallsMoving().ToString();
        gameTimer.text = timeSinceTurn.ToString();

        if (isPlayerTurnActive)
        {
            if (!AnyBallsMoving())
                timeSinceTurn += Time.deltaTime;

            if (!AnyBallsMoving() && playerController.shotMade)
            {
                Debug.Log("changing turn from player shoot");
                //TOFIX: turn ends even if balls are moving on screen
                EndTurn();
                playerController.shotMade = false;
            }

            else if (!AnyBallsMoving() && timeSinceTurn > turnTimer)
            {
                EndTurn();
            }
        }
    }

    public bool AnyBallsMoving()
    {
        if (BallMoving(cueBall))
            return true;

        if (BallMoving(eightBall))
            return true;

        foreach (GameObject ball in stripedBalls)
        {
            if (BallMoving(ball))
                return true;
        }

        foreach (GameObject ball in solidBalls)
        {
            if (BallMoving(ball))
                return true;
        }

        return false;
    }

    private void StartTurn()
    {
        Debug.Log($"starting turn for {currentPlayer}");
        showCurrentPlayer.text = "" + currentPlayer;
        timeSinceTurn = 0.0f;
        isPlayerTurnActive = true;
    }

    private void EndTurn()
    {
        isPlayerTurnActive = false;

        currentPlayer = currentPlayer == player1 ? player2 : player1; //toggle player
        Debug.Log("switching players...");

        StartTurn();

    }
    private bool BallMoving(GameObject ball)
    {
        if (ball != null)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                return rb.velocity.magnitude > 0.05f;
            }
        }
        return false;
    }
}
