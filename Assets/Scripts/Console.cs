using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{

    public float LogScrollSpeed; // Scroll speed of the console log

    private Text ConsoleObject; //Reference to the main console object
    private List<string> ConsoleLog = new List<string>(); //The list which contains all messages sent to the console
    private float ConsoleLogTimer; //timer which manages console scrolling



    // Start is called before the first frame update
    void Start()
    {
        ConsoleObject = this.GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLog();
    }

    /*
    Allows external scripts to access console and dipslay a message
    Main functionality is to test gameplay, and to be adopted to print to specific UI elements
    */
    public void PrintToConsole(string message){
        ConsoleLog.Add(message);
    }

    /*
    Update Log manages the updating of the text box which displays all console logs.
    A user has requested this function is not based on a timer but on user interaction.
    Potentially the logs can also be stored in a seperate variable.
    */
    private void UpdateLog()
    {
        ConsoleLogTimer -= Time.deltaTime;

        string logContainer = "";
        if(ConsoleLog != null)
        {
            foreach(string entry in ConsoleLog)
            {
                logContainer = logContainer + "\n" + entry;
            }

            ConsoleObject.text = logContainer;
        }

        if(ConsoleLogTimer < 0 && logContainer != "")
        {
            if(ConsoleLog.Count > 5){ LogScrollSpeed = 1.0f;} else{LogScrollSpeed = 5.0f;} 
            ConsoleLogTimer = LogScrollSpeed;
            ConsoleLog.Remove(ConsoleLog[0]);
        }
    }



}
