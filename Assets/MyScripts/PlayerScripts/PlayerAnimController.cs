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
    void FixedUpdate()
    {
        
        //switch between idle and run animations.
        if (Input.GetKey(KeyCode.W) && Aim.AimScript.isRotatingTowardsMouse == false || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            playerAnimator.SetBool("isPlayerMoving", true);
        }
        else
        {
            playerAnimator.SetBool("isPlayerMoving", false);
        }

        bool isPlayerTakingCover = TakeCover.TakeCoverScript.isTakingCover;
        if (isPlayerTakingCover)
        {
            playerAnimator.SetBool("isPlayerCovering", true);
        }
        else
        {
            playerAnimator.SetBool("isPlayerCovering", false);
        }

        //aim animation
        bool isPlayerAiming = Aim.AimScript.isRotatingTowardsMouse;
        if (isPlayerAiming)
        {
            playerAnimator.SetBool("isPlayerAiming", true);
            if((Input.GetKey(KeyCode.W)))
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

   
    
}
       
