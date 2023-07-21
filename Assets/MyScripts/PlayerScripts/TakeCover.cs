using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCover : MonoBehaviour
{
    public KeyCode takeCoverkey;
    public bool isNearContainer = false;
    public bool isTakingCover;
    [SerializeField]
    private GameObject Doll;
    Collision ContainerCollision;
    [SerializeField]
    private float rotationSpeed;

    public static TakeCover TakeCoverScript;

    private void Awake()
    {
        TakeCoverScript = this;
    }







    void Start()
    {
        
    }

    void Update()
    {
        if (isNearContainer)
        {
            if (Input.GetKeyDown(takeCoverkey) && PlayerMovement.PlayerMovementScript.isPlayerMoving == false)
            {
                isTakingCover = !isTakingCover; 
            }
        }

        CoverBehaviour();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container"))
        {
            isNearContainer = true;
            ContainerCollision = collision;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container"))
        {
            isNearContainer = false;
            ContainerCollision = null;
        }
    }

    void CoverBehaviour()
    {
        if (isTakingCover)
        {
            PlayerMovement.PlayerMovementScript.enabled = false;
            PlayerRotation.PlayerRotationScript.enabled = false;
            Aim.AimScript.enabled = false;
            rotateOppositeContainer();
            
        }
        else if (!isTakingCover && GunsControl.gunsControlScript.playerHasPistol == true)
        {
            Aim.AimScript.enabled = true;
        }
        else if (Aim.AimScript.isRotatingTowardsMouse == false && !isTakingCover)
        {
            PlayerMovement.PlayerMovementScript.enabled = true;
        }
        
        
       
    }

    void rotateOppositeContainer() //makes the player face the opposite direction of the container.
    {
        if (ContainerCollision != null)
        {
            Vector3 direction = ContainerCollision.GetContact(0).normal;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                Doll.transform.rotation = Quaternion.Lerp(Doll.transform.rotation, targetRotation, rotationSpeed);
            }
        }
           
    }

    
}
