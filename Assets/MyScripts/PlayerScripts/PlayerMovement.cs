using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float moveVelocity; //this is responsible for the player speed.
    private Camera mainCamera;
    Rigidbody rb;
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
            Crouch.crouchScript.isCrouching = false;
            DodgeRoll.dodgeRollScript.isRolling = false;
        }
        else if (!Crouch.crouchScript.isCrouching && !DodgeRoll.dodgeRollScript.isRolling && !Aim.AimScript.isRotatingTowardsMouse)
        {
            moveSpeed = moveVelocity;
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
       
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(moveHorizontal) > 0f || Mathf.Abs(moveVertical) > 0f)
        {
            
            isPlayerMoving = true;
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            movement.Normalize();
            rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
        }
        else
        {
            isPlayerMoving = false;
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }
}