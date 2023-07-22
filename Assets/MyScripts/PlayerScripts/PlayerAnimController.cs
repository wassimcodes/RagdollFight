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

        if (isPlayerTakingCover)
        {
            playerAnimator.SetBool("isPlayerCovering", true);

            if (Input.GetKeyDown(KeyCode.A) && isHorizontalOriginRef)
            {
                playerAnimator.SetBool("isCoverMovingR", true);
                playerAnimator.SetBool("isCoverMovingL", false);

            }
            if (Input.GetKeyDown(KeyCode.A) && isHorizontalReversedRef)
            {
                playerAnimator.SetBool("isCoverMovingL", true);
                playerAnimator.SetBool("isCoverMovingR", false);

            }
            if (Input.GetKeyDown(KeyCode.D) && isHorizontalOriginRef)
            {

                playerAnimator.SetBool("isCoverMovingL", true);
                playerAnimator.SetBool("isCoverMovingR", false);
            }
            if (Input.GetKeyDown(KeyCode.D) && isHorizontalReversedRef)
            {

                playerAnimator.SetBool("isCoverMovingR", true);
                playerAnimator.SetBool("isCoverMovingL", false);
            }

            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                playerAnimator.SetBool("isCoverMovingL", false);
                playerAnimator.SetBool("isCoverMovingR", false);
            }
        }
        else
        {
            playerAnimator.SetBool("isPlayerCovering", false);
            playerAnimator.SetBool("isCoverMovingR", false);
            playerAnimator.SetBool("isCoverMovingL", false);
        }
    }
 
    void PlayerMovementAnim()
    {
        //switch between idle and run animations.
        if (Input.GetKey(KeyCode.W) && Aim.AimScript.isRotatingTowardsMouse == false || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && !TakeCover.TakeCoverScript.isTakingCover)
        {
            playerAnimator.SetBool("isPlayerMoving", true);
        }
        else
        {
            playerAnimator.SetBool("isPlayerMoving", false);
        }
    }

   
    
}
       
