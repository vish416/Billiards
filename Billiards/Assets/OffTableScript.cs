using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffTableScript : MonoBehaviour
{
    private GameManagerScript gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerScript>();

        if (gameManager == null)
        {
            Debug.LogError("no game manager!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameObject ball = other.gameObject;

            gameManager.HandleBallPotted(ball, true);
        }
    }
}
