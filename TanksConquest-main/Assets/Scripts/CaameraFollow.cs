using UnityEngine;

// AUTHOR GROUP 11 UNITY 

/*
 This class handles the camera movement.
 Camera POV follows the tank movement. 
 */

public class CaameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothspeed=0.25f;
    public Vector3 offset;
    public float rotationSpeed = 3.0F;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void LateUpdate()
    {
        transform.LookAt(target.transform);    
    }
}
