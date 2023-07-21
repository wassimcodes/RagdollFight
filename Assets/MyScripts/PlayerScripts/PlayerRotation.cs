using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Speed at which the box rotates
    public static PlayerRotation PlayerRotationScript;
    private void Awake()
    {
        PlayerRotationScript = this;
    }
    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0f || moveVertical != 0f)
        {
            // Get the local movement direction
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            movement.Normalize();

            // Calculate the target rotation based on the local movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            // Apply the rotation to the box's local rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}