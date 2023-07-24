using System.Collections;
using System.Collections.Generic;
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

        //aim animation
        bool isPlayerAiming = Aim.AimScript.isRotatingTowardsMouse;
        if (isPlayerAiming)
        {
            playerAnimator.SetBool("isPlayerAiming", true);
            if ((Input.GetKey(KeyCode.W)))
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && !TakeCover.TakeCoverScript.isTakingCover && Aim.AimScript.isRotatingTowardsMouse == false)
        {
            playerAnimator.SetBool("isPlayerMoving", true);
            

        }
        else
        {
            playerAnimator.SetBool("isPlayerMoving", false);
        }
    }

   
    
}
       
