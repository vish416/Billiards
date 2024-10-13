using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraScript : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    private Vector3 offset;
    public float distanceFromPlayer = 10.0f;
    public float sensitivity = 1000.0f;

    private float currentRotationY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.inShootMode)
        {
            Cursor.lockState = CursorLockMode.Locked;
            float mouseX = Input.GetAxis("Mouse X");

            float rotationAmount = mouseX * Time.deltaTime * sensitivity;

            currentRotationY += rotationAmount;

            Quaternion rotation = Quaternion.Euler(0, currentRotationY, 0);
            transform.position = player.transform.position + rotation * offset;

            transform.LookAt(player.transform);
        }
        else // free up mouse when not shooting
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
