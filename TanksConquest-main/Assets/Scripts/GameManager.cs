using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Collections;


   /* AUTHOR UNITY GROUP 11 
   GameManager class contains the logic for the game. 
   It handles the packets incoming to client. 

   */
public class GameManager : MonoBehaviour
{

    public static List<GameObject> players = new List<GameObject>();
    public static int playersAlive = 3;
    public int localClient;
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject localplayer;
    public GameObject target;



    /// <summary>
    /// Spawning players on the map with preconfigured positions. 
    /// Players are saved into a list for future handling. 
    /// </summary>
    /// <returns>
    /// void
    /// </returns>

    public void SpawnPlayers(int localClient)
    {
        GameObject player;
        this.localClient = localClient;

        List<Vector3> list = new List<Vector3>();

        list.Add(new Vector3((float)-57.7, (float)0.0, (float)47.1));
        list.Add(new Vector3((float)63.6, (float)0.0, (float)47.1));
        list.Add(new Vector3((float)56.5, (float)0.0, (float)-54.4));
        list.Add(new Vector3((float)-59.6, (float)0.0, (float)-54.4));




        for (int i = 1; i <= 3; i++)
        {

            if (i == localClient)
            {
                player = Instantiate(localPlayerPrefab, list[i - 1], Quaternion.identity) as GameObject;
                localplayer = player;
            }
            else
            {
                player = Instantiate(playerPrefab, list[i - 1], Quaternion.identity) as GameObject;
            }

            players.Add(player);

        }
    }

    /// <summary>
    /// reads packets and handles the positioning, shooting on the map. 
    /// </summary>
    /// <returns>
    /// void
    /// </returns>

    public void movePlayers(byte[] newPos)
    {


        int clientID = readPacket.toInt(newPos, 44);

        if (clientID > 0)
        {
            if (players[clientID - 1] != null)
            {

                Vector3 vect = new Vector3(newPos.toFloat(0), newPos.toFloat(4), newPos.toFloat(8));
                Quaternion receivedRotation = new Quaternion(newPos.toFloat(12), newPos.toFloat(16), newPos.toFloat(20), newPos.toFloat(24));
                Quaternion receivedTurretRotation = new Quaternion(newPos.toFloat(28), newPos.toFloat(32), newPos.toFloat(36), newPos.toFloat(40));
                bool isFiring = newPos.toBool(48);

                if (clientID != localClient)
                {
                    players[(clientID - 1)].transform.position = vect;
                    players[(clientID - 1)].transform.rotation = receivedRotation;
                    players[(clientID - 1)].transform.GetChild(3).GetChild(0).transform.rotation = receivedTurretRotation;


                    if (isFiring)
                    {
                        Instantiate(target, players[(clientID - 1)].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).TransformPoint(Vector3.forward * 5), players[(clientID - 1)].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).rotation);
                        Debug.Log("Firing:" + isFiring);
                    }
                }
            }
        }

    }

}

