using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AUTHOR GROUP 11 UNITY 

/*
 this class manages the shell physics. 
 Built-in destruction timer for the shell. 
 */

public class Shell : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject Bullet;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        // Obtain the reference to our Rigidbody.
        body = GetComponent<Rigidbody>();

        
        Destroy(gameObject, 3.0f);
    }

    void OnTriggerEnter(Collider collison)
    {
        Destroy(this.Bullet);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = (transform.forward * 200) * (speed * 50) * Time.fixedDeltaTime;
        velocity.y = body.velocity.y;
        body.velocity = velocity;
        
    }
}
