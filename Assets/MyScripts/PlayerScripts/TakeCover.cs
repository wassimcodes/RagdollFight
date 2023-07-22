using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCover : MonoBehaviour
{
    public KeyCode takeCoverkey;
    public bool isNearContainer = false;
    public bool isTakingCover;
    [SerializeField] private GameObject Doll;
    [SerializeField] private Transform dollTransform;
    private Vector3 InitialDollTransform;
    Collision ContainerCollision;
    [SerializeField] private float rotationSpeed;

    public static TakeCover TakeCoverScript;
    public float coverSpeedX;
    public float coverSpeedZ;

    public bool isHorizontalOrigin; //player cover looking at the camera
    public bool isHorizontalReversed; //player cover looking at the other side

 
    private void Awake()
    {
        TakeCoverScript = this;
    }







    void Start()
    {
        InitialDollTransform = transform.localPosition;
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

        //check player rotation
        if (dollTransform.localEulerAngles.y < 153 && dollTransform.localEulerAngles.y > 152)
        {
            isHorizontalOrigin = true;
        }
        else
        {
            isHorizontalOrigin = false;
        }
        if (dollTransform.localEulerAngles.y < 333 && dollTransform.localEulerAngles.y > 332)
        {
            isHorizontalReversed = true;
        }
        else
        {
            isHorizontalReversed = false;
        }
    

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
            playerCoverMovement();
            
        }
        else if (!isTakingCover && GunsControl.gunsControlScript.playerHasPistol == true)
        {
            Aim.AimScript.enabled = true;
        }
        else if (Aim.AimScript.isRotatingTowardsMouse == false && !isTakingCover)
        {

            PlayerMovement.PlayerMovementScript.enabled = true;
        }
        if (!isNearContainer)
        {
            isTakingCover = false;
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

    void playerCoverMovement()
    {
        if (isHorizontalOrigin || isHorizontalReversed)
        {
            
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float movementAmount = horizontalInput * coverSpeedX * Time.deltaTime;
            Vector3 localChildPosition = transform.InverseTransformPoint(dollTransform.position);

            Vector3 targetPosition = transform.localPosition;
            targetPosition.x += localChildPosition.x * movementAmount;
            transform.localPosition = targetPosition;
        }
        
        //the z movement of the player while taking cover.
         if ((dollTransform.localEulerAngles.y < 243 && dollTransform.localEulerAngles.y > 242 || dollTransform.localEulerAngles.y < 63 && dollTransform.localEulerAngles.y > 62))
        {
           
            float verticalInput = Input.GetAxisRaw("Vertical");
            float movementAmount = verticalInput * coverSpeedZ * Time.deltaTime;
            Vector3 localChildPosition = transform.InverseTransformPoint(dollTransform.position);

            Vector3 targetPosition = transform.localPosition;
            targetPosition.z += localChildPosition.z * movementAmount;
            transform.localPosition = targetPosition;
        }


        
        




    }
    
    
}
