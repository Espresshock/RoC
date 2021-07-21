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

    private KeyCode TradeKey = KeyCode.X;
    //Write function to generate, get and set keycode later



    private List<ResourceScriptableObject> PlayerResources = new List<ResourceScriptableObject>();
    private TradeOfferScriptableObject Offer;
    private TradeOfferScriptableObject Request;


    //Phase and turn management: Turn Counter, phase enumerator, timer and time tracking
    //Player Interaction Management: Input detection, Bind event to input 
    //Feedback Management: Send text to console, enable/disable ui and audio
    //Score Management: Coin variable, Debt variable, 'resource' counters



    // Start is called before the first frame update
    void Start()
    {

        bPhaseInProgress = false;
        InitializePlayerResources();
        Console.LogScrollSpeed = 3;
        //GameStartEvent
        //initializeplayer
        StartCoroutine(StartGame());
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

        if (PhaseTimer <= 0 && bPhaseInProgress)
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
                InitializeContractPhase();
                break;
            case 1:
                InitializeTradePhase();
                break;
            case 2:
                InitializeOrderPhase();
                break;
            case 3:
                InitializeEndOfDay();
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
        if (bPhaseInProgress)
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
                    if (Input.GetKeyDown(TradeKey) && InputTimer <= 0)
                    {
                        ExecuteTradeTransaction();
                    }


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

    private void PrintPlayerResources()
    {
        Console.PrintToConsole("You now have the following resources:" + "\n");
        foreach (ResourceScriptableObject resource in PlayerResources)
        {
            Console.PrintToConsole("A total of: " + resource.ResourceQuantity + " " + resource.ResourceName);
        }
    }

    private void InitializePlayerResources()
    {

        ResourceScriptableObject StartCoin = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        StartCoin.SetResource(StartCoin, ResourceScriptableObject.RESOURCE.COIN, 10);
        PlayerResources.Add(StartCoin);

        ResourceScriptableObject StartScrap = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        StartCoin.SetResource(StartScrap, ResourceScriptableObject.RESOURCE.SCRAP, 10);
        PlayerResources.Add(StartScrap);

    }

    private IEnumerator StartGame()
    {

        //Startgame requires rewrite as game phase and should toggle through a loop of strings which the player can progress using space bar.
        Console.PrintToConsole("Starting Game");
        Console.LogScrollSpeed = 3;
        Console.PrintToConsole("Thank you for playing the first playable version River of Coin. \n ");
        yield return new WaitForSeconds(3);
        Console.PrintToConsole("This game is Katelyst's entry in the RNDGAME JAM II. \n \n");
        yield return new WaitForSeconds(3);
        Console.PrintToConsole("The River of Coin is home to a vast variety of traders, savy merchants, and customers. \n");
        yield return new WaitForSeconds(3);
        Console.PrintToConsole("Reeti Recently bought a shop with the ambition of ascending up the river of coin, You can help by managing some of the day to day tasks.\n");
        yield return new WaitForSeconds(5);
        Console.PrintToConsole("Please reach out with any comments, we are excited to hear what you think! \n The game is currently limited in functionality, but aims to depict some core ideas. \n The Game loop will Start in a few seconds.");
        yield return new WaitForSeconds(5);
        Console.PrintToConsole("You can Press Space Bar to skip through phases. or wait for them to pass");
        yield return new WaitForSeconds(10);
        Console.LogScrollSpeed = 1;
        PhaseTimer = 10;
        bPhaseInProgress = true;

    }


    private void InitializeTradePhase()
    {
        Console.PrintToConsole("Time to trade! \n");
        Console.LogScrollSpeed = 3;

        Offer = TradeManager.GenerateTradeOffer(CurrentTurn, PlayerResources);
        TradeKey = TradeManager.GenerateTradeKey();

        Console.PrintToConsole("The astute " + Offer.MerchantName + ", Requests a grand total of: " + Offer.ResourcesRequested.ResourceQuantity + ", of your " + Offer.ResourcesRequested.ResourceName + "\n");
        Console.PrintToConsole("In Exchange for their: " + Offer.ResourcesOffered.ResourceQuantity + " " + Offer.ResourcesOffered.ResourceName + "\n");
        Console.PrintToConsole("Too Agree with this offer, Press: " + TradeKey.ToString());

    }

    private void ExecuteTradeTransaction()
    {
        print(Offer);
        print(Offer.ResourcesOffered.ResourceName + Offer.ResourcesOffered.ResourceQuantity);
        print(Offer.ResourcesRequested.ResourceName + Offer.ResourcesRequested.ResourceQuantity); 

        List<ResourceScriptableObject> NewPlayerResources = Offer.AcceptTradeOffer(PlayerResources);
        PlayerResources = NewPlayerResources;

        Console.PrintToConsole("You Accepted the merchants offer!" + "\n");
        PrintPlayerResources();

        ScriptableObject.Destroy(Offer);
    }





    private void InitializeEndOfDay()
    {
        Console.PrintToConsole("The Day has come to an end, The debt collector takes his cut \n");
        foreach (ResourceScriptableObject resource in PlayerResources)
        {
            int debt = 1;
            if (resource.ResourceName == "Coin")
            {
                resource.ResourceQuantity = resource.ResourceQuantity - debt;
                Console.PrintToConsole("He takes: " + debt + " " + resource.ResourceName + " As his payment. \n");
                PrintPlayerResources();
            }
        }
    }

    private void InitializeContractPhase()
    {
        Console.PrintToConsole("New Contract Offers have Arrived. \n This phase is currently not functional");
    }

    private void InitializeOrderPhase()
    {
        Console.PrintToConsole("A few orders became available. \n This phase is currently not functional \n");
    }



}

