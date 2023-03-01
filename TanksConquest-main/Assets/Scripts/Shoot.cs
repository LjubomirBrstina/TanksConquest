using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AUTHOR GROUP 11 UNITY 

/*
 this class is event handler (mouse click). 
 */



public class Shoot : MonoBehaviour
{
    public GameObject target;
    public float FireRate  = 1;
    public float NextFire = 0;
    public bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Listens once per frame for mouse input (click). 
    /// If mouse clicked = spawn shel object from the tank turret. 
    /// </summary>
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Time.time > NextFire)
        {
            
            NextFire = Time.time + FireRate;
            Instantiate(target, transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).TransformPoint(Vector3.forward * 5), transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).rotation);
            isFiring = true;
        }
        else
        isFiring = false;
        //coroutine();
        
    }

    IEnumerator coroutine()
    {
        yield return new WaitForSeconds(1);
        isFiring = false;
    }
    
}
