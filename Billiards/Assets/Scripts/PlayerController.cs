using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speedScale = 0;
    public float knockX = 0;
    public float knockZ = 0;
    public float knockScale = 1;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private bool knock = false;

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

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void Update()
    {
        if (knock)
        {
            Knock();
            knock = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speedScale);
    

    }


    private void Knock()
    {
        Vector3 knockMove = new Vector3(knockX, 0.0f, knockZ);
        rb.AddForce(knockMove * knockScale, ForceMode.Impulse);
        //rb.MovePosition(transform.position + knockMove * knockScale * Time.deltaTime);
    }
}
