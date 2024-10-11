using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public Camera topCamera;
    public Camera ballCamera;

    private bool onPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onPlayer = !player.inShootMode;
        topCamera.enabled = onPlayer;
        ballCamera.enabled = !onPlayer;
    }
}
