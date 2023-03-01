using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;

/* AUTHOR UNITY GROUP 11 
   
  Dedicated Server class for the game. 
  Handles all the packet between the clients. 
  Receives packets from clients and forwards it to the rest of the clients. 
  */

namespace NetworktingTest
{
    
    class ServerUDP
    {
        public Socket sock;
        byte[] recivedData = new byte[1024];
        IPEndPoint serverEP;
        EndPoint storedClientEP;
        ArrayList clientList;
        int maxplayers = 3;
        bool everyoneConnected = false;



        /// <summary>
        /// Creates and initializes the server socket with UDP protocol. 
        /// Creates a client list for storing the clients. 
        /// </summary>
        /// <param name="IPAdress">The ip adress.</param>
        /// <param name="port">The port.</param>
        /// <returns>
        /// void
        /// </returns>
        public void initServer(String IPAdress, int port)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverEP = new IPEndPoint(IPAddress.Any, port);
            sock.Bind(serverEP);
            storedClientEP = (EndPoint) serverEP;
            clientList = new ArrayList();
        }



        /// <summary>
        /// Recieves the data from the socket.
        /// Stores the packet endpoint and calls the addClient method. 
        /// If all clients are connected but not in-game - send start game message. 
        /// 
        /// </summary>
        public void recieveData()
        {
       
            sock.ReceiveFrom(recivedData, ref storedClientEP);
            String package = Encoding.ASCII.GetString(recivedData, 0, 1);
                addClient(); 
            
                
            if(clientList.Count == maxplayers && everyoneConnected==false)
            {
                everyoneConnected = true;
                SendToAll("Start game");
            }
            
            if (everyoneConnected)
            {
                for(int i=0; i<maxplayers; i++)
                {
                    if(!storedClientEP.Equals(clientList[i]))
                    {
                        Console.WriteLine("Sending positions to Client: " + (i+1));
                        sock.SendTo(recivedData, recivedData.Length, SocketFlags.None, (EndPoint)clientList[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether the endpoint is stored. 
        /// If not --> store it in client list.
        /// Assign the clientID to the new client. 
        /// </summary>
        /// 
        public void addClient()
        {
            if(!clientList.Contains(storedClientEP))
            {
                clientList.Add(storedClientEP);
                Console.WriteLine("Added Client " + clientList.Count);
                SendToClient(Convert.ToString(clientList.Count));
            }     
        }

        /// <summary>
        /// Encodes string to byte data and sends to latest client.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendToClient(String message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            //Console.WriteLine("Sending to " + storedClientEP.ToString());
            sock.SendTo(data, data.Length, SocketFlags.None, storedClientEP);
        }



        /// <summary>
        /// Sends starts message to all clients. 
        /// This method is called when all the clients have connected to server. 
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendToAll(String message)
        {
            for (int i = 0; i < maxplayers; i++)
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                Console.WriteLine("Sending to client :spawn");
                sock.SendTo(data, data.Length, SocketFlags.None, (EndPoint) clientList[i]);
            }
        }

        /// <summary>
        /// Resets the server after game is finished.
        /// </summary>
        public void ResetServer()
        {
            clientList = new ArrayList();
            everyoneConnected = false;
        }
    }
}
