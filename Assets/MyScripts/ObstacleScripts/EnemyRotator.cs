using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    void Start()
    {
        drawALine();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        Debug.Log("i'm working");
    }

    void drawALine()
    {
        Debug.DrawLine(transform.position, Vector3.forward * 50f, Color.red);

    }


}
