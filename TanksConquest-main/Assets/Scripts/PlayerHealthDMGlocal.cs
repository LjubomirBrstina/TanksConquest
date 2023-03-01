using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// AUTHOR GROUP 11 UNITY 

/*
 This class handles the health points for the local player. 
 It uses collision detection and invokes unity methods for FX. 
 */


public class PlayerHealthDMGlocal : MonoBehaviour
{
    public Slider healthBar;
    public GameObject SpectatorCamera;
    public int hp = 3;
    public GameObject Bullet;
    public Transform target;
    public GameObject explosionEffect; 


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
    /// If player is dead initialize spectator mode. 
    /// </summary>
    /// <param name="other">The other.</param>
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (transform.gameObject.tag == Bullet.tag)
        {
            hp--;
            healthBar.value--;
            Explode();
        }




        if (hp <= 0)
        {
            GameManager.playersAlive--;
            Destroy(transform.gameObject);
            Instantiate(SpectatorCamera, SpectatorCamera.transform.position, SpectatorCamera.transform.rotation);
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
    }


}
