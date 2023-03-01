using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AUTHOR GROUP 11 UNITY 

/*
 This class is event handler for keyboard input. 
 */



public class TankMovement : MonoBehaviour
{
    
    private Rigidbody body;
    public float speed;
    public float rotationSpeed;
    private float vertical;
    private float horizontal;
    
    void Start()
    {
        
        body = GetComponent<Rigidbody>();

    }
    /// <summary>
    /// Loops once per frame. 
    /// Uses keyboard input listeners and rotates the tank accordingly. 
    /// </summary>
    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Vector3 velocity = (transform.forward * vertical) * (speed*50) * Time.fixedDeltaTime;
        velocity.y = body.velocity.y;
        body.velocity = velocity;
        transform.Rotate((transform.up * horizontal) * (rotationSpeed*20) * Time.fixedDeltaTime);
    }
    
}
