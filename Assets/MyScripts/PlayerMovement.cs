using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Camera mainCamera;
    private Rigidbody rb;
    private Animator playerAnimator;
  
    public static PlayerMovement PlayerMovementScript;



    private void Awake()
    {
        PlayerMovementScript = this;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main;

        playerAnimator = GetComponentInChildren<Animator>();
    }



    private void Update()
    {

        //check if the player is pressing any buttons if he does then it will play running animation, if he doesnt it will play the idle animation.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
            playerAnimator.SetBool("isPlayerMoving", true);
            }
        else
        { 
            playerAnimator.SetBool("isPlayerMoving", false);
        }
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
            rb.velocity = movement * moveSpeed;            
        }
        else
        {
            // Stop the player instantly
            rb.velocity = Vector3.zero;         
        }
    }
}
