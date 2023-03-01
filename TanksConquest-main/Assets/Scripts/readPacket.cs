using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

// AUTHOR GROUP 11 UNITY 


/* 
 This class packs and unpacks byte arrays. 
 Used by client to pack local player data variables. 
 Used by GameManager to unpack packets. 
 */


public static class readPacket
{
    //Anv�nds n�r vi packar ihop ett paket
    public static byte[] toByteArray(this float value)
    {
        return BitConverter.GetBytes(value);
    }

    public static byte[] toByteArray(this int value)
    {
        return BitConverter.GetBytes(value);
    }

    public static byte toByte(this bool value)
    {
        return (byte)(value ? 1 : 0);
    }



    //anv�nds n�r vi l�ser av paket.
    public static float toFloat(this byte[] bytes, int startIndex)
    {
        return BitConverter.ToSingle(bytes, startIndex);
    }

    public static int toInt(this byte[] bytes, int startIndex)
    {
        return BitConverter.ToInt32(bytes, startIndex);
    }

    public static bool toBool(this byte[] bytes, int startIndex)
    {
        return bytes[startIndex] == 1;
    }
}
