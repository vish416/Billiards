using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public string player1 = "PPPPPlayer 1";
    public string player2 = "Player 2";

    public string currentPlayer;

    public PlayerController playerController;
    public UIManagerScript uiManager;

    public GameObject cueBall;
    public GameObject eightBall;

    public string stripedPlayer = "";
    public List<GameObject> stripedBalls;
    public string solidPlayer = "";
    public List<GameObject> solidBalls;

    public Text gameResult;

    public delegate void BallPotEvent(GameObject ball);
    public BallPotEvent OnBallPot;

    private const float MAX_TURN_TIME = 30.0f;
    private float timeSinceTurn = 0.0f;
    public bool isPlayerTurnActive = false;
    private bool isGameActive = true;
    public bool ballPotted = false;


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
        //early return if game over
        if (!isGameActive)
        {
            return;
        }

        uiManager.UpdateTurnTimer(timeSinceTurn);

        if (isPlayerTurnActive)
        {
            if (!AnyBallsMoving())
                timeSinceTurn += Time.deltaTime;

            if (AnyBallsMoving() && playerController.shotMade)
            {
                Debug.Log("changing turn from player shoot");
                isPlayerTurnActive = false;
            }

            else if (!AnyBallsMoving() && timeSinceTurn > MAX_TURN_TIME)
            {
                Debug.Log("current player ran out of time, swapping player");
                EndTurn();
            }
        }
        else
        {
            if (!AnyBallsMoving())
            {
                if (!ballPotted)
                {
                    Debug.Log("shot made with no ball potted, swapping player");
                    EndTurn();
                }
                else
                {
                    timeSinceTurn = 0;
                    ballPotted = false;
                    isPlayerTurnActive = true;
                }
                playerController.shotMade = false;
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

    public void HandleBallPotted(GameObject ball)
    {
        Debug.Log("ball potted" + ball);
        if (ball == cueBall)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
            ball.transform.position = new Vector3(3.60999894f, 4.75f, -0.60995698f);
                
            rb.velocity = Vector3.zero;
            
            return;
        }

        else if (ball == eightBall)
        {
            Debug.Log("eight ball potted");
            if (currentPlayer == stripedPlayer)
            {
                Debug.Log("striped player potted 8ball");
                if (stripedBalls.Count == 0)
                {
                    // win event goes here
                    gameResult.text = "" + currentPlayer + " wins!";
                }
                else
                {
                    //lose event goes here
                    gameResult.text = "" + ((currentPlayer == player1) ? player2 : player1) + " wins!";
                }
            }
            else if (currentPlayer == solidPlayer)
            {
                Debug.Log("solid player potted 8ball");
                if (solidBalls.Count == 0)
                {
                    // win event goes here
                    gameResult.text = "" + currentPlayer + " wins!";
                }
                else
                {
                    //lose event goes here
                    gameResult.text = "" + ((currentPlayer == player1) ? player2 : player1) + " wins!";
                }
            }
            else if (currentPlayer != stripedPlayer || currentPlayer != solidPlayer) //edge case where current player pots 8 ball without potting any other ball
            {
                //lose event goes here
                gameResult.text = "" + ((currentPlayer == player1) ? player2 : player1) + " wins!";
            }

            return;
        }

        else if (stripedBalls.Contains(ball))
        {
            Debug.Log("striped ball potted");
            if (stripedPlayer == "")
            {
                stripedPlayer = currentPlayer;
                solidPlayer = (currentPlayer == player1) ? player2 : player1;
                ballPotted = true;
            }
            else if (currentPlayer == stripedPlayer)
            {
                ballPotted = true;
            }

            stripedBalls.Remove(ball);
            Destroy(ball);
            return;
        }

        else if (solidBalls.Contains(ball))
        {
            Debug.Log("solid ball potted");
            if (solidPlayer == "")
            {
                solidPlayer = currentPlayer;
                stripedPlayer = (currentPlayer == player1) ? player2 : player1;
                ballPotted = true;
            }
            else if (currentPlayer == solidPlayer)
            {
                ballPotted = true;
            }

            solidBalls.Remove(ball);
            Destroy(ball);
            return;
        }
    }
}
