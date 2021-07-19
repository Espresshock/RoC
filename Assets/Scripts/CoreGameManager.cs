using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameManager : MonoBehaviour
{

    public Console Console;

    public int CurrentTurn;

    public List<float> PhaseDurationArray = new List<float>();

    public List<KeyCode> SelectionInputOptions = new List<KeyCode>();
    public int CoinBalance;


    private int CurrentPhase;
    //0 = Contract, 1 = Trade, 2 = Order, 3 = End
    private bool bPhaseInProgress;
    private int TotalTurns;
    private float PhaseTimer;

    //Phase and turn management: Turn Counter, phase enumerator, timer and time tracking
    //Player Interaction Management: Input detection, Bind event to input 
    //Feedback Management: Send text to console, enable/disable ui and audio
    //Score Management: Coin variable, Debt variable, 'resource' counters



    // Start is called before the first frame update
    void Start()
    {
        CurrentPhase = -1;
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
                break;
            case 1:
            Console.PrintToConsole("Time to trade!");
            //List<Gameobject> TradeOffers = Trademanager.GenerateTrades(CoinBalance, Resources);
            //foreach Offer in TradeOffers
                //Console.PrintToConsole(TradeOffers[Offer].TradeOfferString);
                break;
            case 2:
                break;
            case 3:
                break;
        }

        PhaseTimer = PhaseDurationArray[phase];
        print(phase);
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
        switch (CurrentPhase)
        {
            case 0: 
            // Generate key bindings for contracts
            // foreach contract in TotalContracts
                // SelectionInputOptions[rand(0,3)] = Offer.AcceptContract();
                break;
            case 1: 
            // Generate key bindings for trade
            // foreach Trade in TotalTrades
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndCurrentPhase();
        }
    }
}

