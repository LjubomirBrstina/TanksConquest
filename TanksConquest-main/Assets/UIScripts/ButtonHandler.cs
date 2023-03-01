using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// AUTHOR GROUP 11 UNITY 

/*
 This class handles the buttons in the start menu. 
 */

public class ButtonHandler : MonoBehaviour
{
    GameObject IPinput;

    private void Start()
    {
        IPinput = GameObject.Find("IP");
    }
    public void PlayGame()
    {
    
        SceneManager.LoadScene(1);   
    }

    public void QuitGame()
    {
        
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
