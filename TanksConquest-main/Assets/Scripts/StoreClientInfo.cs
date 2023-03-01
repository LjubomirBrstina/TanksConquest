using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// AUTHOR GROUP 11 UNITY 

/*
 This class stores the data input from the start menu. 
 Used by client class to establish connection. 
 */



public class StoreClientInfo : MonoBehaviour
{
    GameObject IPinput;
    GameObject ServerPort;
    GameObject LocalPort;

    public string ipaddress;
    public int serverPort;
    public int localPort;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IPinput = GameObject.Find("IP");
        ServerPort = GameObject.Find("ServerPort");
        LocalPort = GameObject.Find("LocalPort");

        if (IPinput!=null)
        {
            ipaddress = IPinput.GetComponent<InputField>().text;
            //Debug.Log(ipaddress);
        }

        if (ServerPort != null)
        {
            if(ServerPort.GetComponent<InputField>().text.Length>0)
            {
                string input = ServerPort.GetComponent<InputField>().text;
                serverPort = int.Parse(input);
            }
            
            //Debug.Log(ipaddress);
        }

        if (LocalPort != null)
        {
            if(LocalPort.GetComponent<InputField>().text.Length>0)
            {
                string input = LocalPort.GetComponent<InputField>().text;
                localPort = int.Parse(input);
            }
            

            
            //Debug.Log(ipaddress);
        }

        /*if(ipaddress!=null)
        {
            Debug.Log(ipaddress);
        }*/

    }
}
