using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCoverAnim();
        
        PlayerMovementAnim();

        PlayerCrouchAnim();

        PlayerDodgeRollAnim();

        //aim animation
        bool isPlayerAiming = Aim.AimScript.isRotatingTowardsMouse;
        bool isHoldingPistol = GunsControl.gunsControlScript.playerHasPistol;
        bool isHoldingAk = GunsControl.gunsControlScript.playerHasAk;

        //holding pistol
        if (isPlayerAiming && isHoldingPistol)
        {
            playerAnimator.SetBool("isPlayerAiming", true);
            playerAnimator.SetBool("isHoldingPistol", true);

            if (PlayerMovement.PlayerMovementScript.isPlayerMoving)
            {
                playerAnimator.SetBool("isPlayerMoving", true);
            }
            else
            {
                playerAnimator.SetBool("isPlayerMoving", false);
            }
        }
        else
        {
            playerAnimator.SetBool("isPlayerAiming", false);
            playerAnimator.SetBool("isHoldingPistol", false);
        }

        //holding Ak
        if (isPlayerAiming && isHoldingAk)
        {
            playerAnimator.SetBool("isPlayerAiming", true);
            playerAnimator.SetBool("isHoldingAk", true);
            if (PlayerMovement.PlayerMovementScript.isPlayerMoving)
            {
                playerAnimator.SetBool("isPlayerMoving", true);
            }
            else
            {
                playerAnimator.SetBool("isPlayerMoving", false);
            }
        }
        else
        {
            playerAnimator.SetBool("isPlayerAiming", false);
            playerAnimator.SetBool("isHoldingAk", false);
        }
    }

   void PlayerCoverAnim()
    {
        bool isHorizontalOriginRef = TakeCover.TakeCoverScript.isHorizontalOrigin;
        bool isHorizontalReversedRef = TakeCover.TakeCoverScript.isHorizontalReversed;
        bool isPlayerTakingCover = TakeCover.TakeCoverScript.isTakingCover;
        bool isVerticalOriginRef = TakeCover.TakeCoverScript.isVerticalOrigin;
        bool isVerticalReversedRef = TakeCover.TakeCoverScript.isVerticalReversed;

        if (isPlayerTakingCover)
        {
            playerAnimator.SetBool("isPlayerCovering", true);


            // cover animation controller
            if (Input.GetKeyDown(KeyCode.A) && isHorizontalOriginRef || Input.GetKeyDown(KeyCode.W) && isVerticalOriginRef)
            {
                playerAnimator.SetBool("isCoverMovingR", true);
                playerAnimator.SetBool("isCoverMovingL", false);
               

            }
            if (Input.GetKeyDown(KeyCode.A) && isHorizontalReversedRef || Input.GetKeyDown(KeyCode.W) && isVerticalReversedRef)
            {
                playerAnimator.SetBool("isCoverMovingL", true);
                playerAnimator.SetBool("isCoverMovingR", false);
               
            }
            if (Input.GetKeyDown(KeyCode.D) && isHorizontalOriginRef || Input.GetKeyDown(KeyCode.S) && isVerticalOriginRef)
            {
                playerAnimator.SetBool("isCoverMovingL", true);
                playerAnimator.SetBool("isCoverMovingR", false);
               
            }
            if (Input.GetKeyDown(KeyCode.D) && isHorizontalReversedRef || Input.GetKeyDown(KeyCode.S) && isVerticalReversedRef)
            {

                playerAnimator.SetBool("isCoverMovingR", true);
                playerAnimator.SetBool("isCoverMovingL", false);
               
            }

            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                if (isHorizontalOriginRef || isHorizontalReversedRef)
                {
                    playerAnimator.SetBool("isCoverMovingL", false);
                    playerAnimator.SetBool("isCoverMovingR", false);
                }  
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                if (isVerticalOriginRef || isVerticalReversedRef)
                {
                    playerAnimator.SetBool("isCoverMovingL", false);
                    playerAnimator.SetBool("isCoverMovingR", false);
                }
            }

        }
        else
        {
           playerAnimator.SetBool("isPlayerCovering", false);
            playerAnimator.SetBool("isCoverMovingR", false);
            playerAnimator.SetBool("isCoverMovingL", false);
            playerAnimator.SetBool("isPlayerMoving", false);
        }
    }
 
    void PlayerMovementAnim()
    {
        //switch between idle and run animations.
        if (PlayerMovement.PlayerMovementScript.isPlayerMoving && !TakeCover.TakeCoverScript.isTakingCover && !Aim.AimScript.isRotatingTowardsMouse)
        {
            playerAnimator.SetBool("isPlayerMoving", true);
        }
        else
        {
            playerAnimator.SetBool("isPlayerMoving", false);
        }
    }

    void PlayerCrouchAnim()
    {
        if (Crouch.crouchScript.isCrouching && !PlayerMovement.PlayerMovementScript.isPlayerMoving)
        {
            playerAnimator.SetBool("isCrouching", true);
        }
        else
        {
            playerAnimator.SetBool("isCrouching", false);
        }

        if (Crouch.crouchScript.isCrouching && PlayerMovement.PlayerMovementScript.isPlayerMoving)
        {
            playerAnimator.SetBool("isCrouching", true);
            playerAnimator.SetBool("isPlayerMoving", true);
        }
    }

    void PlayerDodgeRollAnim()
    {

        if (DodgeRoll.dodgeRollScript.isRolling)
        {
            if (PlayerMovement.PlayerMovementScript.isPlayerMoving)
            {
                playerAnimator.SetBool("isRolling", true);
                playerAnimator.SetBool("isPlayerMoving", true);
            }
            else if (!PlayerMovement.PlayerMovementScript.isPlayerMoving)
            {
                playerAnimator.SetBool("isRolling", false);
                playerAnimator.SetBool("isPlayerMoving", false);
            }  
        }

        else
        {
            playerAnimator.SetBool("isRolling", false);
        }
        
    }
}
       