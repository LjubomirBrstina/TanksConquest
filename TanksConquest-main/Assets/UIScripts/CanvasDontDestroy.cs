using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AUTHOR GROUP 11 UNITY 

/*
 This class makes the StoreClientInfo game object persist during scene change.  
 */
public class CanvasDontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
