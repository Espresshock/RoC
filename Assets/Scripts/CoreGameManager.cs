using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameManager : MonoBehaviour
{

    public Console Console; //Reference to the Text Console which prints messages to the player
    
    public InputHandler InputHandler;
    
    public GameStateMachine GameStateMachine;

    //PlayerResourceInterface ResourceInterfaceReference
    
    public List<ResourceScriptableObject> PlayerResources = new List<ResourceScriptableObject>(); //List of player resources
    
    //0 = Contract, 1 = Trade, 2 = Order, 3 = End

    public PlayerResourcesInterface ResourceInterfaceReference;
    
    private int TotalTurns; //Measures total number of turns that have passed.
    
    void Start()
    {       
        
        Console.PrintToConsole("Starting Game");
        GameStateMachine.StartGame();
        Console.LogScrollSpeed = 3;

    }


    void Update()
    {
        GameStateMachine.PhaseUpdate();  
        InputHandler.CheckPlayerInput(); 
    }  
   



}

