using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFly : MonoBehaviour
{
    public float boxVelocity;
    [SerializeField] bool movingToEnd = true;
    public int ForceValue;

    public static BoxFly BoxFlyScript;
    private void Awake()
    {
        BoxFlyScript= this;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the movement vector
        float targetX = movingToEnd ? -35f : -24.79f;

        // Move the box towards the target position
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), boxVelocity * Time.deltaTime);

        // Check if the box has reached the target position
        if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
        {
            // Reverse the direction
            movingToEnd = !movingToEnd;
        }

        //check if the box is moving left or right and calculate the force's direction depending on the box's movement.
        if(movingToEnd)
        {
            ForceValue = -100;
        }
        else
        {
            ForceValue = 100;
        }
    }
}
