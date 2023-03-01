using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AUTHOR GROUP 11 UNITY 

/*
 This class is event handler for mouse input (rotation). 
 */



public class TowerRotate : MonoBehaviour
{
    public float rotationSpeed = 3.0F;

    /// <summary>
    /// Loops once per frame. 
    /// Listens for mouse input rotation and rotates the tank cannon accordingly. 
    /// </summary>
    void Update()
    {
        float x = rotationSpeed * Input.GetAxis("Mouse X");

        transform.Rotate(0, 0, x);
    }
}
