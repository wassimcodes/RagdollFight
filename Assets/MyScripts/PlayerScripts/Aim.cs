using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    private Camera mainCamera;
    public bool isRotatingTowardsMouse;
    public float moveSpeedAiming;
    private Transform parentTransform;

    // Move and rotation settings
    public float rotationSpeed = 10f;

    // Aim zoom
    public float smoothSpeed;
    public float targetSize;

    private float originalSize;
    public bool isZoomingIn = false;
    private bool isZoomingOut = false;


    public static Aim AimScript;

    private void Awake()
    {
        AimScript = this;
       
    }


    private void Start()
    {
        mainCamera = Camera.main;
        originalSize = mainCamera.orthographicSize;
        targetSize = originalSize - 1f;
        parentTransform = transform.parent;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotatingTowardsMouse = true;
            isZoomingIn = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotatingTowardsMouse = false;
            isZoomingOut = true;
        }

        
    }

    private void FixedUpdate()
    {
        if (isZoomingIn)
        {
            ZoomIn();
        }
        else if (isZoomingOut)
        {
            ZoomOut();
        }

        if (isRotatingTowardsMouse)
        {
            RotateTowardsMouse();
            //moveTowardsMouse();
            PlayerRotation.PlayerRotationScript.enabled = false;
   
        }
        else if (!isRotatingTowardsMouse && !Crouch.crouchScript.isCrouching)
        {
            PlayerMovement.PlayerMovementScript.enabled = true;
            PlayerRotation.PlayerRotationScript.enabled = true;
        }
    }

    private void RotateTowardsMouse()
    {
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit))
        {
            
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f; 

            
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    //void moveTowardsMouse()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            Vector3 cursorPosition = hit.point;
    //            cursorPosition.y = parentTransform.position.y; 
    //            Vector3 moveDirection = (cursorPosition - parentTransform.position).normalized;
    //            moveDirection.y = 0f;
    //            parentTransform.position += moveDirection * moveSpeedAiming * Time.deltaTime;
    //        }
    //    }
    //}

    private void ZoomIn()
    {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, smoothSpeed * Time.deltaTime);

        // If the difference between the current size and the target size is small enough, stop zooming in
        if (Mathf.Abs(mainCamera.orthographicSize - targetSize) < 0.01f)
        {
            isZoomingIn = false;
        }
    }

    private void ZoomOut()
    {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalSize, smoothSpeed * Time.deltaTime);

        // If the difference between the current size and the original size is small enough, stop zooming out
        if (Mathf.Abs(mainCamera.orthographicSize - originalSize) < 0.01f)
        {
            isZoomingOut = false;
        }
    }


}