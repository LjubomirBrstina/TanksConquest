using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public int health;
    public Vector3 position;
    public Quaternion rotation;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    float horizontalInput;

    public Player(int id, Vector3 spawnPosition)
    {
        this.id = id;
        position = spawnPosition;
        rotation = Quaternion.identity;
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, horizontalInput, 0);

        Vector3 test = new Vector3(0, 0, 0);
        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey("down"))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
    }
}
