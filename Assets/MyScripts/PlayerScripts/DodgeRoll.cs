using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    public bool isRolling = false;
    public KeyCode rollKey;
    public float rollDuration;
    [SerializeField] float rollSpeed;
    public static DodgeRoll dodgeRollScript;


    private void Awake()
    {
        dodgeRollScript = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(rollKey) && PlayerMovement.PlayerMovementScript.isPlayerMoving)
        {
            isRolling = true;
            Crouch.crouchScript.enabled = false;
            StartCoroutine (StopRolling());
        }

        velocityController();
    }

    private System.Collections.IEnumerator StopRolling() //makes the player roll for a certain duration then toggle the isRolling to false.
    {
        yield return new WaitForSeconds(rollDuration);
        isRolling = false;
        Crouch.crouchScript.enabled = true;
    }

    void velocityController()
    {
       float playerRunningSpeed = PlayerMovement.PlayerMovementScript.moveVelocity;

        if (isRolling && !TakeCover.TakeCoverScript.isTakingCover) 
        {
            PlayerMovement.PlayerMovementScript.moveSpeed = rollSpeed;
            Aim.AimScript.enabled = false;
        }

        
        else if (!Crouch.crouchScript.isCrouching && !isRolling)
        {
            PlayerMovement.PlayerMovementScript.moveSpeed = playerRunningSpeed;
        }
    }
}
