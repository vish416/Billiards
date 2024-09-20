using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallScript : MonoBehaviour
{
    public PlayerController player;
    public GameManagerScript gameManager;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(gameManager.isPlayerTurnActive);

        if (player.mouseTracking)
        {
            //add cue pullback here
        }
    }
}
