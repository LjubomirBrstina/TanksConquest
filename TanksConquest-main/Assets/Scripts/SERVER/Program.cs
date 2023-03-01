using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

 /* AUTHOR UNITY GROUP 11 
  Creates a server instance and handles the data in a loop.  
  
  */

namespace NetworktingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // change to desired server IP address and respective server port on the hosting machine 
            // do port-forward on your machine aswell 
            ServerUDP myServer = new ServerUDP();
            myServer.initServer("192.168.0.19", 9050);
         
            while(true)
            {
                
                myServer.recieveData();
               

            }
            Console.ReadKey();
        }
    }
}
