using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    public float moveVelocity; //this is responsible for the player speed.
    private Camera mainCamera;
    private Rigidbody rb;
    public bool isPlayerMoving;
    public static PlayerMovement PlayerMovementScript;



    private void Awake()
    {
        PlayerMovementScript = this;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main;
    }



    private void Update()
    {

        //change player speed while aiming
        if (Aim.AimScript.isRotatingTowardsMouse)
        {
            moveSpeed = Aim.AimScript.moveSpeedAiming;
        }
        else
            moveSpeed = moveVelocity;
    }




    private void FixedUpdate()
    {

        DefaultWalk();
        PlayerRotation.PlayerRotationScript.enabled = true;


        if (Aim.AimScript.isRotatingTowardsMouse)
        {
            PlayerRotation.PlayerRotationScript.enabled = false;

        }
    }


    //responsible for the player's local movement
    private void DefaultWalk()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Get the local right and forward vectors
        Vector3 localRight = transform.right;
        Vector3 localForward = transform.forward;

        // Calculate the movement vector based on local axes
        Vector3 movement = (localRight * moveHorizontal) + (localForward * moveVertical);

        if (movement.magnitude > .5f)
            movement.Normalize();

        // Check if there is any input to move
        if (Mathf.Abs(moveHorizontal) > 0f || Mathf.Abs(moveVertical) > 0f)
        {
            // Apply movement to the box's rigidbody
            isPlayerMoving = true;
            rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);

        }
        else
        {
            // Stop the player instantly
            isPlayerMoving = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}