using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class GameManagerScript : MonoBehaviour
{

    public string player1 = "PPPPPlayer 1";
    public string player2 = "Player 2";

    public string currentPlayer;

    public PlayerController playerController;
    public BallDisplayScript ballDisplay;

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
    public bool isGameActive = true;
    public bool ballPotted = false;

    public statisticsScript stats;

    // Start is called before the first frame update
    void Start()
    {
	GetDisplayName();
	if (stats == null)
        {
            stats = FindObjectOfType<statisticsScript>(); 
            if (stats == null)
            {
                Debug.LogError("statisticsScript not found! Please assign it in the Inspector.");
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //early return if game over
        if (!isGameActive)
        {
            return;
        }

        ballDisplay.SetBallList(GetActiveBalls());

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

    // For turn timer progress bar:
    public float GetTurnRatio()
    {
        return timeSinceTurn / MAX_TURN_TIME;
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
        ballDisplay.SetBallList(GetActiveBalls());

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

    public void HandleBallPotted(GameObject ball, bool isFoul)
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
            
            if (eightBall == null) // edge case where cueball is potted after eightball
            {
                //WEIRD win event goes here
                gameResult.text = "" + ((currentPlayer == player1) ? player2 : player1) + " wins!";
                EndGame();
            }

            return;
        }

        else if (ball == eightBall)
        {
            Debug.Log("eight ball potted");
            if (currentPlayer == stripedPlayer)
            {
                Debug.Log("striped player potted 8ball");
                if (stripedBalls.Count == 0 && !isFoul)
                {
                    // win event goes here
                    gameResult.text = "" + currentPlayer + " wins!";
					HandleGameResult();
                    EndGame();
                }
                else
                {
                    //lose event goes here
                    gameResult.text = "" + ((currentPlayer == player1) ? player2 : player1) + " wins!";
					HandleGameResult();
                    EndGame();
                }
            }
            else if (currentPlayer == solidPlayer)
            {
                Debug.Log("solid player potted 8ball");
                if (solidBalls.Count == 0 && !isFoul)
                {
                    // win event goes here
                    gameResult.text = "" + currentPlayer + " wins!";
					HandleGameResult();
                    EndGame();
                }
                else
                {
                    //lose event goes here
                    gameResult.text = "" + ((currentPlayer == player1) ? player2 : player1) + " wins!";
					HandleGameResult();
                    EndGame();
                }
            }
            else if (currentPlayer != stripedPlayer || currentPlayer != solidPlayer) //edge case where current player pots 8 ball without potting any other ball
            {
                //lose event goes here
                gameResult.text = "" + ((currentPlayer == player1) ? player2 : player1) + " wins!";
				HandleGameResult();
                EndGame();
            }

            Destroy(ball);
            return;
        }

        else if (stripedBalls.Contains(ball))
        {
            Debug.Log("striped ball potted");
            if (stripedPlayer == "")
            {
                if (!isFoul)
                {
                    stripedPlayer = currentPlayer;
                    solidPlayer = (currentPlayer == player1) ? player2 : player1;
                    ballPotted = true;
                }
                else
                {
                    stripedPlayer = (currentPlayer == player1) ? player2 : player1;
                    solidPlayer = currentPlayer;
                }
            }
            else if (currentPlayer == stripedPlayer && !isFoul)
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
                if (!isFoul)
                {
                    solidPlayer = currentPlayer;
                    stripedPlayer = (currentPlayer == player1) ? player2 : player1;
                    ballPotted = true;
                }
                else
                {
                    solidPlayer = (currentPlayer == player1) ? player2 : player1;
                    stripedPlayer = currentPlayer;
                }
            }
            else if (currentPlayer == solidPlayer && !isFoul)
            {
                ballPotted = true;
            }

            solidBalls.Remove(ball);
            Destroy(ball);
            return;
        }
    }

    public List<GameObject> GetActiveBalls()
    {
        //check if either player 'owns' solid/stripes
        if (stripedPlayer == "")
        {
            return new List<GameObject>();
        }

        if (currentPlayer == stripedPlayer)
        {
            if (stripedBalls.Count == 0)
            {
                return new List<GameObject> { eightBall }; ;
            }
            else 
            {
                return stripedBalls;
            }
        }
        else
        {
            if (solidBalls.Count == 0)
            {
                return new List<GameObject> { eightBall }; ;
            }
            else
            {
                return solidBalls;
            }
        }
    }

    void GetDisplayName()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), (result) => {
            string currentDisplayName = result.AccountInfo.TitleInfo.DisplayName;
            player1 = currentDisplayName;
	    currentPlayer = player1;
            StartTurn();


            Debug.Log(currentPlayer);
            Debug.Log("starting game");
        }, OnError);
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }

    void HandleGameResult()
    {
        if (stats != null)  
        {
            if (gameResult.text.Contains(player1)) 
            {
                stats.addWin();
                Debug.Log("Win added to stats.");
            }
            else  
            {
                stats.addLoss();
                Debug.Log("Loss added to stats.");
            }
        }
        else
        {
            Debug.LogError("Error: statisticsScript is not assigned.");
        }
    public void EndGame()
    {
        //clear the ball display
        ballDisplay.SetBallList(new List<GameObject>());
        isGameActive = false;
    }
}
