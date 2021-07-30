using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{

    private Text ConsoleObject; //Reference to the main console object



    // Start is called before the first frame update
    void Awake()
    {
        ConsoleObject = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    Allows external scripts to access console and dipslay a message
    Main functionality is to test gameplay, and to be adopted to print to specific UI elements
    */
    public void PrintToConsole(string message)
    {
        if(ConsoleObject != null)
        {
        ConsoleObject.text = message;
        }
    }

    /*
    Update Log manages the updating of the text box which displays all console logs.
    A user has requested this function is not based on a timer but on user interaction.
    Potentially the logs can also be stored in a seperate variable.
    */


}
