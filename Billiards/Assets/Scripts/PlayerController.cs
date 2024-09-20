using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject playerCamera;
    public GameManagerScript gameManager;

    public float speedScale = 0;
    public float knockX = 0;
    public float knockZ = 0;
    public float knockScale = 100.0f;

    public bool shotMade = false;

    private Rigidbody rb;

    private bool knock = false;

    public float mouseDistance = 0.0f;
    public bool mouseTracking = false;

    [ContextMenu ("Knock Ball")]
    public void doKnock()
    {
        knock = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (knock)
        {
            Knock();
            knock = false;
        }

        if (!gameManager.AnyBallsMoving())
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseTracking = true;
                mouseDistance = 0;
            }

            if (mouseTracking)
            {
                mouseDistance += Input.GetAxis("Mouse Y");

                if (Input.GetMouseButtonUp(0))
                {
                    //dont allow 'backwards' shots
                    if (mouseDistance < 0)
                    {
                        if (!gameManager.AnyBallsMoving())
                        {
                            CameraKnock();
                            shotMade = true;
                        }
                    }


                    Debug.Log($"{mouseDistance} knock");
                    mouseTracking = false;
                }
            }
        }
    }

    void FixedUpdate()
    {

    }


    private void Knock()
    {
        Vector3 knockMove = new Vector3(knockX, 0.0f, knockZ);
        rb.AddForce(knockMove * knockScale, ForceMode.Impulse);
    }

    private void CameraKnock()
    {
        Vector3 direction = playerCamera.transform.forward;
        direction.y = 0.0f;

        rb.AddForce(direction * -mouseDistance, ForceMode.Impulse);
    }
}
