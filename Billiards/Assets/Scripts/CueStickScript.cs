using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallScript : MonoBehaviour
{
    public GameObject cueBall;
    public PlayerController player;
    public GameManagerScript gameManager;

    private float originalOffset;
    public float maxBackDistance = 5.0f;

    private const float MAX_STICKDISTANCE = 1.4f;
    private float distance;
    private float distanceScalar;

    // Start is called before the first frame update
    void Start()
    {
        originalOffset = Vector3.Distance(cueBall.transform.position, transform.position);
        distanceScalar = MAX_STICKDISTANCE / PlayerController.MAX_POWER;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = gameManager.isPlayerTurnActive;
        }

        //prevent cue stick from clipping with ball on shot release (only update distance while shot is being made)
        if (player.mouseTracking)
        {
            distance = player.mouseDistance;
        }

        Vector3 moveDirection = transform.forward;
        //set pos relative to cueball: add original offset in direction of cue stick,
        //subtract by cuestick direction scaled by distance of player shot (with distance scalar to ensure cue stick does not leave camera view
        transform.position = cueBall.transform.position + (moveDirection * originalOffset) - (distanceScalar * distance * moveDirection);
    }

}
