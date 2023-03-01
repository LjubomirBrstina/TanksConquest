using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// AUTHOR GROUP 11 UNITY 

/* Client class sends and retrieves packets from the Server class. 
 * It contains an inner class that handles the connection to the server. 
 */



public class Client : MonoBehaviour
{
    public GameManager gm;
    public ClientUDP myClient;
    public Transform target;
    public bool connected = false;

    /// <summary>
    /// This mathod handles the communication with the server. 
    /// It uses UdpClient class from .NET framework. 
    /// </summary>
    
    public class ClientUDP
    {
        public int clientID;
        byte[] sendData = new byte[1024];
        public byte[] recivedData = new byte[1024];
        public byte[] recivedInitClientData = new byte[1024];
        int hostPort;
        public int localPort;
        String hostIP;
        public IPEndPoint ServerEP;
        public UdpClient client;


        /// <summary>
        /// Initializes the client.
        /// </summary>
        /// <param name="ipaddress">The ipaddress.</param>
        /// <param name="hostPort">The host port.</param>
        /// <param name="localPort">The local port.</param>
        /// 
        public void initClient(String ipaddress, int hostPort, int localPort)
        {
            try
            {
                this.hostPort = hostPort;
                this.localPort = localPort;
                hostIP = ipaddress;
                client = new UdpClient(localPort);
                ServerEP = new IPEndPoint(IPAddress.Any, 0);

                SendToServer("Connected");

                //Recieve client ID
                recivedInitClientData = client.Receive(ref ServerEP);
                clientID = int.Parse(Encoding.ASCII.GetString(recivedInitClientData));

                //Recieve Start game command
                recivedInitClientData = client.Receive(ref ServerEP);

            }
            catch (Exception)
            {
                Debug.Log("Horrible things happened!");
            }
        }

        /// <summary>
        /// this method sends data to the server. 
        /// It uses an server IP adress and server port. 
        /// Method is used only when establishing connection. 
        /// </summary>
        /// <param name="Data"></param>
        /// <returns>
        /// void
        /// </returns>


        public void SendToServer(String Data)
        {
            this.sendData = Encoding.ASCII.GetBytes(Data);
            client.Send(sendData, sendData.Length, hostIP, hostPort);
        }


        /// <summary>
        ///this method sends data to the server. 
        /// It uses an server IP adress and server port.
        /// Method used during the game. 
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns>
        /// void
        /// </returns>
        
        public void SendToServerBytes(Byte[] Data)
        {
            this.sendData = Data;
            if (client != null)
            {
                client.Send(sendData, sendData.Length, hostIP, hostPort);
            }
                
        }


    }


    /// <summary>
    /// Receives data if it is available on UdpClient from the server.
    /// Calls the GameManager movePlayer method with the retrieved data.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>


    public void RecieveFromServer()
    {
        if (this.myClient.client.Client.Available > 0)
        {
            this.myClient.recivedData = myClient.client.Receive(ref this.myClient.ServerEP);
            gm.movePlayers(this.myClient.recivedData);

        }

    }


    /// <summary>
    /// Starts this instance. Uses the provided client parameters from the start screen. 
    /// Calls the GameManagers SpawnPlayers method, after the connection to the server is established. 
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    
    void Start()
    {
        string IP = GameObject.Find("StoreClientInfo").GetComponent<StoreClientInfo>().ipaddress;
        int serverPort = GameObject.Find("StoreClientInfo").GetComponent<StoreClientInfo>().serverPort;
        int localPort = GameObject.Find("StoreClientInfo").GetComponent<StoreClientInfo>().localPort;


        this.myClient = new ClientUDP();
        this.myClient.initClient(IP, serverPort, localPort);

        connected = true;
        Debug.Log("My clientID: " + myClient.clientID);



        gm.SpawnPlayers(myClient.clientID);
    }





    /// <summary>
    /// Packs the client's local player data into a byte array. 
    /// </summary>
    /// <returns>
    /// byte array 
    /// </returns>
    


    public Byte[] packData()
    {
        byte[] packedData = new byte[1024];
        Buffer.BlockCopy(gm.localplayer.transform.position.x.toByteArray(), 0, packedData, 0, 4); //X  POSITION
        Buffer.BlockCopy(gm.localplayer.transform.position.y.toByteArray(), 0, packedData, 4, 4); //Y
        Buffer.BlockCopy(gm.localplayer.transform.position.z.toByteArray(), 0, packedData, 8, 4); //Z

        Buffer.BlockCopy(gm.localplayer.transform.rotation.x.toByteArray(), 0, packedData, 12, 4); //X ROTATION
        Buffer.BlockCopy(gm.localplayer.transform.rotation.y.toByteArray(), 0, packedData, 16, 4); //Y
        Buffer.BlockCopy(gm.localplayer.transform.rotation.z.toByteArray(), 0, packedData, 20, 4); //Z
        Buffer.BlockCopy(gm.localplayer.transform.rotation.w.toByteArray(), 0, packedData, 24, 4); //W

        Buffer.BlockCopy(gm.localplayer.transform.GetChild(3).GetChild(0).transform.rotation.x.toByteArray(), 0, packedData, 28, 4); //tower rotation
        Buffer.BlockCopy(gm.localplayer.transform.GetChild(3).GetChild(0).transform.rotation.y.toByteArray(), 0, packedData, 32, 4); //tower rotation
        Buffer.BlockCopy(gm.localplayer.transform.GetChild(3).GetChild(0).transform.rotation.z.toByteArray(), 0, packedData, 36, 4); //tower rotation
        Buffer.BlockCopy(gm.localplayer.transform.GetChild(3).GetChild(0).transform.rotation.w.toByteArray(), 0, packedData, 40, 4); //tower rotation

        Buffer.BlockCopy(myClient.clientID.toByteArray(), 0, packedData, 44, 4); //clientID
        packedData[48] = gm.localplayer.GetComponent<Shoot>().isFiring.toByte(); //isFiring

        return packedData;
    }

    /// <summary>
    /// Updates this instance by looping once per frame. 
    /// Receives data from the server and sends data if local player is alive. 
    /// The last player alive messages the server that the game round is finished. 
    /// If finished = close socket and load the start screen again for the players. 
    /// 
    /// </summary>
    /// <returns>
    /// void
    /// </returns>


    void Update()
    {
        
        if (GameManager.playersAlive==1)
        {

            
            if(gm.localplayer!=null)
            {
                
                    byte[] data = Encoding.ASCII.GetBytes("G");
                    this.myClient.SendToServerBytes(data);
                    Debug.Log("I told server that the game is over.");
                
            }
            
            Debug.Log("Game has ended, socket is closed now.");

            
            GameManager.playersAlive = 2;
            this.myClient.client.Close();
            connected = false;


            SceneManager.LoadScene(0);
            Cursor.visible = true;
            Debug.Log("Start scene loaded");
        }
        
       
        if(connected==true)
        {
            if (gm.localplayer != null)
            {
                this.myClient.SendToServerBytes(packData());
            }
            RecieveFromServer();
        }
   
    }
}