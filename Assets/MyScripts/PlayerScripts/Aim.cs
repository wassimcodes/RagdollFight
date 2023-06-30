using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    private Camera mainCamera;
    public bool isRotatingTowardsMouse;
    private Transform playerParentTransform;
    public float moveSpeedAiming;

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
        playerParentTransform = transform.parent;
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
            moveTowardsMouse();
            PlayerMovement.PlayerMovementScript.enabled = false;
            PlayerRotation.PlayerRotationScript.enabled = false;
        }
        else if (!isRotatingTowardsMouse)
        {
            PlayerMovement.PlayerMovementScript.enabled = true;
            PlayerRotation.PlayerRotationScript.enabled = true;
        }
    }

    private void RotateTowardsMouse()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray intersects with a collider
        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the direction from the object to the hit point
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f; // Ignore the y-axis component

            // Calculate the rotation to look at the hit point
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the object towards the hit point
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void moveTowardsMouse()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Get the cursor position in world space
                Vector3 cursorPosition = hit.point;
                cursorPosition.y = playerParentTransform.position.y; // Keep the same Y position as the parent

                // Calculate the direction towards the cursor position
                Vector3 moveDirection = (cursorPosition - playerParentTransform.position).normalized;

                // Set the movement only on the X and Z axes
                moveDirection.y = 0f;

                // Move the parent object towards the cursor position
               playerParentTransform.position += moveDirection * moveSpeedAiming * Time.deltaTime;
            }
        }
    }

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