using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{

    public float LogScrollSpeed;

    private Text ConsoleObject;
    private List<string> ConsoleLog = new List<string>();
    private float ConsoleLogTimer;



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

    
    public void PrintToConsole(string message){
        ConsoleLog.Add(message);
    }

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
