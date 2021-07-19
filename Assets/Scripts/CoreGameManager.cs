using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameManager : MonoBehaviour
{

    public Console Console;
    public TradeGameManager TradeManager;
    public int CurrentTurn;
    public List<float> PhaseDurationArray = new List<float>();
    public List<KeyCode> SelectionInputOptions = new List<KeyCode>();
    public int CoinBalance;
    public float InputCooldown;


    private int CurrentPhase;
    //0 = Contract, 1 = Trade, 2 = Order, 3 = End
    private bool bPhaseInProgress;
    private int TotalTurns;
    private float PhaseTimer;
    private float InputTimer;

    //Phase and turn management: Turn Counter, phase enumerator, timer and time tracking
    //Player Interaction Management: Input detection, Bind event to input 
    //Feedback Management: Send text to console, enable/disable ui and audio
    //Score Management: Coin variable, Debt variable, 'resource' counters



    // Start is called before the first frame update
    void Start()
    {
        CurrentPhase = -1;
        InputTimer = InputCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        PhaseUpdate();
        CheckPlayerInput();           
    }

    private void PhaseUpdate()
    {
        if (bPhaseInProgress)
        {
            PhaseTimer -= Time.deltaTime;
        }

        if (PhaseTimer <= 0)
        {
            EndCurrentPhase();
        }
    }


    private void EndCurrentPhase()
    {
        bPhaseInProgress = false;
        Console.PrintToConsole("Phase: " + CurrentPhase + " Has Ended.");
        //ResolveCurrentPhase;
        //FeedbackUpdate

        if (CurrentPhase < 3)
        {
            CurrentPhase += 1;
            StartPhase(CurrentPhase);

        }
        else
        {
            EndCurrentTurn();
        }
    }

    private void StartPhase(int phase)
    {
        Console.PrintToConsole("Starting Phase: " + phase);
        switch (phase)
        {
            case 0:
            Console.PrintToConsole("New Contract Offers have Arrived.");
                break;
            case 1:
            Console.PrintToConsole("Time to trade!");
            print(TradeManager);
            
            TradeOfferScriptableObject Offer = TradeManager.GenerateTradeOffer();

            Console.PrintToConsole("The astute " + Offer.MerchantName + ", Requests a grand total of: " + Offer.ResourcesRequested.ResourceQuantity + ", of your " + Offer.ResourcesRequested.ResourceName + "\n");
            Console.PrintToConsole("In Exchange for their: " + Offer.ResourcesOffered.ResourceQuantity + " " + Offer.ResourcesOffered.ResourceName + "\n"); 
            Console.PrintToConsole("Too Agree with this offer, Press: ");

                break;
            case 2:
            Console.PrintToConsole("A few orders became available.");
                break;
            case 3:
            Console.PrintToConsole("The Day has come to an end, The debt collector takes his cut");
                break;
        }

        PhaseTimer = PhaseDurationArray[phase];
        bPhaseInProgress = true;

    }


    private void EndCurrentTurn()
    {
        // ResolveTurn
        // Feedback / Score Card
        CurrentPhase = 0;
        Console.PrintToConsole("End of turn: " + CurrentTurn);
        CurrentTurn += 1;
        StartPhase(CurrentPhase);
    }


    private void CheckPlayerInput()
    {
        InputTimer -= Time.deltaTime;
        switch (CurrentPhase)
        {
            case 0: 
            // Generate key bindings for contracts
            // foreach contract in TotalContracts
                // SelectionInputOptions[rand(0,3)] = Offer.AcceptContract();
                break;
            case 1: 
            // Generate key bindings for trade
            List<KeyCode> TradeKeys = new List<KeyCode>();
            TradeKeys.Add(KeyCode.Z);
            TradeKeys.Add(KeyCode.X);
            TradeKeys.Add(KeyCode.C);
            
                // SelectionInputOptions[rand(0,3)] = Trade.AcceptTrade();
                break;
            case 2: 
            // Generate key bindings for trade
            // foreach orders in TotalOrders
                // SelectionInputOptions[rand(0,3)] = Order.AcceptOrder();
                break;
            case 3: 
            // disable input during end phase.
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space) && InputTimer <= 0)
        {
            print(InputTimer);
            EndCurrentPhase();
            InputTimer = InputCooldown;
        }
    }
}

