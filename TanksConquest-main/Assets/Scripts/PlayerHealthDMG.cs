using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// AUTHOR GROUP 11 UNITY 

/*
 This class handles the health points for the enemy players. 
 It uses collision detection and invokes unity methods for FX. 
 */
public class PlayerHealthDMG : MonoBehaviour
{
    public Slider healthBar;
    public int hp = 3;
    public GameObject Bullet;
    public Transform target;


    public GameObject explosionEffect;   // skapa en explosion 
    public float blastRadius = 5f;
    public float force = 70000f; 

    public void Start()
    {
        healthBar.value = hp;
    }

    public void Update()
    {
        healthBar.transform.rotation = new Quaternion(0, target.rotation.y, 0, target.rotation.w);
    }

    /// <summary>
    /// Called when [trigger enter].
    /// Checks if the tank is hit by a shell. 
    /// If hit = reduced HP by 1 unit and destroys the shell. 
    /// If tank HP = 0 --> destroy tank and reduce GameManager's playersAlive var. 
    /// </summary>
    /// <param name="other">The other.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if(transform.gameObject.tag == Bullet.tag)
        {
            hp--;
            healthBar.value--;
            Explode(); 


        }
        
        Destroy(other.gameObject);
        if (hp <= 0)
        {
            GameManager.playersAlive--;
            Destroy(transform.gameObject);
            
        }
    }

    /// <summary>
    /// Initiate explosion FX.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(explosionEffect, 1.0f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);   // getting all nearby objects 
        // looking for a rigidbody object
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, blastRadius);  // apply force
            }

            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null)

            {
                dest.Destroy(); 
            }
        
        }
    
    }





}

