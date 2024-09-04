using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public float distanceFromPlayer = 10.0f;
    public float sensitivity = 1000.0f;

    private float currentRotationY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");

        float rotationAmount = mouseX * Time.deltaTime * sensitivity;

        currentRotationY += rotationAmount;

        Quaternion rotation = Quaternion.Euler(0, currentRotationY, 0);
        transform.position = player.transform.position + rotation * offset;

        transform.LookAt(player.transform);
    }
}
