using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] private KeyCode crouchKey;
    [SerializeField] private float newCapsuleHeight;
    [SerializeField] private float newCapsuleCenterY;
    private float originalCapsuleHeight;
    private float originalCapsuleCenterY;
    public bool isCrouching = false;
    public float CrouchSpeed; 
    private CapsuleCollider capsuleCollider;

    public static Crouch crouchScript;


    private void Awake()
    {
        crouchScript = this;
    }


    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalCapsuleCenterY = GetComponent<CapsuleCollider>().center.y;
        originalCapsuleHeight = GetComponent<CapsuleCollider>().height;
    }

   
    void Update()
    {
        bool isMoving = PlayerMovement.PlayerMovementScript.isPlayerMoving;
        bool isAiming = Aim.AimScript.isRotatingTowardsMouse;
        if (Input.GetKeyDown(crouchKey) && !isAiming && !isMoving)
        {
            isCrouching = !isCrouching;
        }
       

        CapsuleCrouchSettings();
        CrouchVelocityController();
        crouchController();
    }

    void CapsuleCrouchSettings()
    {
        if (isCrouching)
        {
            capsuleCollider.height = newCapsuleHeight;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, newCapsuleCenterY, capsuleCollider.center.z);
        }        
        else
        {
            capsuleCollider.height = originalCapsuleHeight;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, originalCapsuleCenterY, capsuleCollider.center.z);
        }
    }

    void CrouchVelocityController()
    {
        if (isCrouching)
        {
            PlayerMovement.PlayerMovementScript.moveSpeed = CrouchSpeed;
            
        }
        else if (!DodgeRoll.dodgeRollScript.isRolling && !isCrouching)
        {
            float RunningSpeed = PlayerMovement.PlayerMovementScript.moveVelocity;
            PlayerMovement.PlayerMovementScript.moveSpeed = RunningSpeed;
        }
    }

    void crouchController()
    {
        if (isCrouching)
        {
            DodgeRoll.dodgeRollScript.enabled = false;
        }

        else if (!isCrouching && !TakeCover.TakeCoverScript.isTakingCover)
        {
            DodgeRoll.dodgeRollScript.enabled = true;
        }
    }
}
