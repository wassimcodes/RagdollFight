using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    public bool isRolling = false;
    public KeyCode rollKey;
    public float rollDuration;
    [SerializeField] float coolDownRoll;
    [SerializeField] float rollSpeed;
    [SerializeField] bool canRoll; //to avoid spamming the key
    public static DodgeRoll dodgeRollScript;
    


    private void Awake()
    {
        dodgeRollScript = this;
    }

    void Start()
    {
        canRoll = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(rollKey) && PlayerMovement.PlayerMovementScript.isPlayerMoving && canRoll)
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
        canRoll = false;

        yield return new WaitForSeconds(coolDownRoll);
        canRoll = true;
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
