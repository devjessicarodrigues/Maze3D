using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    public Transform imageTarget; 
    public Rigidbody ballRigidbody; 
    public float movementSpeed = 3f; 
    public Collider mazeBounds; 

    private Vector3 previousPosition;

    void Start()
    {
        if (imageTarget != null)
        {
            previousPosition = imageTarget.position;
        }
    }

    void FixedUpdate()
    {
        if (imageTarget != null && ballRigidbody != null)
        {
            Vector3 deltaPosition = imageTarget.position - previousPosition;

            Vector3 force = new Vector3(deltaPosition.x, 0, deltaPosition.z) * movementSpeed;
            ballRigidbody.AddForce(force, ForceMode.VelocityChange);

            previousPosition = imageTarget.position;

            ConfineToMaze();
        }
    }

    void ConfineToMaze()
    {
        if (mazeBounds != null)
        {
            Vector3 ballPosition = ballRigidbody.position;
            Vector3 closestPoint = mazeBounds.ClosestPoint(ballPosition);

            if (Vector3.Distance(ballPosition, closestPoint) > 0.01f)
            {
                ballRigidbody.position = closestPoint;
                ballRigidbody.velocity = Vector3.zero; 
            }
        }
    }
}
