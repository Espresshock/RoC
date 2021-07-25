using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameManager : MonoBehaviour
{

    public Console Console; //Reference to the Text Console which prints messages to the player
    public TradeGameManager TradeManager; //Reference to the trade manager component. Handles generation of trade offers and supporting elements
    public int CurrentTurn; // current player turn.
    public List<float> PhaseDurationArray = new List<float>(); //Publicly adjustable list of the number and duration of phases.
    public List<KeyCode> SelectionInputOptions = new List<KeyCode>(); //Container for potential keys to be used to interact with systems
    public int CoinBalance; //Number of coins the player has
    public float InputCooldown; // Cooldown before individual input actions can take place.


    public int CurrentPhase;
    //0 = Contract, 1 = Trade, 2 = Order, 3 = End
    private bool bPhaseInProgress; //Checks whether phase is currently progressing.
    private int TotalTurns; //Measures total number of turns that have passed.
    private float PhaseTimer; //Tracks time of phase.
    private float InputTimer; //Tracks time since last input.

    private int StartGamePhase;
    private bool SpaceIsPressed;

    private KeyCode TradeKey; //Holds the key to interact with the active trade. TradeKey should be generated using a function/

    private List<TradeOfferScriptableObject> OfferList;

    private PlayerResourcesInterface ResourceInterfaceReference;



    private List<ResourceScriptableObject> PlayerResources = new List<ResourceScriptableObject>(); //List of player resources
    private TradeOfferScriptableObject Offer; //Reference to current trade offer



    /*
        The start sequence should run an event which introduces the player to the world and the goal.
        Currently this event is a timer based prompt, user would like this to become button press event. Change StartGame function to phase -1 in turn 0.
        Start() only sets bphaseinprogress - false, current phase to -1, and turn to 0. StartGame() than runs in phase update and check player input.

        Develop a function in the console which toggles the scrolling instead of a timer based system.
    */
    void Start()
    {
        StartGamePhase = 0;
        bPhaseInProgress = false;
        CurrentPhase = -1;        
        InputTimer = InputCooldown;
        Console.PrintToConsole("Starting Game");
        StartGame();
        Console.LogScrollSpeed = 3;

    }


    void Update()
    {
        PhaseUpdate();  
        CheckPlayerInput(); 
    }  
        
    /*
    Phase Update Manages the progression of phases .
    */
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

    /*
    EndCurrentPhase Communicates a phase has ended, 
    determines whether to start a next phase, 
    or to end the turn and reset phases.
    */
    private void EndCurrentPhase()
    {
        bPhaseInProgress = false;
        ResolveCurrentPhase();
        

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

    /*
    Start Phase determines which phase to initiate based on the phase variable. 
    It then sets the phase time to the appropriately assigned duration. 
    It also starts the phase timer

    Contract Phase:
    Player is offered a selection of 'resources' to deliver within a certain number of 'turns' to receive 'resources'
    
    Trade Phase:
    Player can directly trade a selection of 'resources' for a selection of other 'resources

    Order Phase:
    Plauer is offered a selection of 'resources' in a number of 'turns', in return for a selection of their 'resources' now.

    End Phase:
    Debt Collector takes % of remaining debt interest in coins.
    if < x Game Over.
    */
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

    /*
        Ends the current phase and executes any functions which have to be fired at the end of a phase.
    */
    private void ResolveCurrentPhase()
    {
        Console.PrintToConsole("Phase: " + CurrentPhase + " Has Ended.");
        if(OfferList != null)
        {
            ResourceInterfaceReference.UpdateResourceInterface(PlayerResources);
            TradeManager.DestroyRemainingCards();
        }
    }

    /*
        Ends the current turn and resets the phase.
        // TO-DO: ResolveTurn
        // TO-DO: Feedback / Score Card
        // Startt phase 0 of next turn.
    */
    private void EndCurrentTurn()
    {
        CurrentPhase = 0;
        Console.PrintToConsole("End of turn: " + CurrentTurn);
        CurrentTurn += 1;
        StartPhase(CurrentPhase);
    }

    /*
    If a phase is in progress, check player input waits to receive any of the phase valid input options.
    At all times, check player input waits to receive space bar input to end the current phase.
    Check player input may be transfered to a seperate object which will handle this gameplay, along with any control assignment functions.
    */
    private void CheckPlayerInput()
    {
        if (bPhaseInProgress)
        {
            InputTimer -= Time.deltaTime;
            switch (CurrentPhase)
            {
                case 0:
                    // Contract Phase

                    break;
                case 1:
                    // TradePhase
                    foreach(TradeOfferScriptableObject Offer in OfferList)
                    {
                        if (Input.GetKeyDown(Offer.TradeKey) && InputTimer <= 0)
                        {
                            ExecuteTradeTransaction(Offer);
                        }
                    }
                    

                    break;
                case 2:
                    // Order Phase

                    break;
                case 3:
                    // End Phase

                    break;
            }

            if (Input.GetKeyDown(KeyCode.Space) && InputTimer <= 0 && bPhaseInProgress)
            {
                print(InputTimer);
                EndCurrentPhase();
                InputTimer = InputCooldown;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) && StartGamePhase < 6)
                {
                    StartGame();
                    InputTimer = InputCooldown;
                }
        }
    }

    /*
        Prints the contents of the playerResources list to the console
    */
    private void PrintPlayerResources()
    {
        Console.PrintToConsole("You now have the following resources:" + "\n");
        foreach (ResourceScriptableObject resource in PlayerResources)
        {
            Console.PrintToConsole("A total of: " + resource.ResourceQuantity + " " + resource.ResourceName);
        }
    }

    /*
        Initializes the player with a certain amount of resources when the game starts.
    */
    private void InitializePlayerResources()
    {

        ResourceScriptableObject StartCoin = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        StartCoin.SetResource(StartCoin, ResourceScriptableObject.RESOURCE.COIN, 10);
        PlayerResources.Add(StartCoin);

        ResourceScriptableObject StartScrap = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        StartCoin.SetResource(StartScrap, ResourceScriptableObject.RESOURCE.SCRAP, 10);
        PlayerResources.Add(StartScrap);

        ResourceInterfaceReference = GetComponentInChildren<PlayerResourcesInterface>();
        ResourceInterfaceReference.InitializeResourceInterface();
        ResourceInterfaceReference.UpdateResourceInterface(PlayerResources);

    }


    /*
        The start sequence should run an event which introduces the player to the world and the goal.
        Currently this event is a timer based prompt, user would like this to become button press event. Change StartGame function to phase -1 in turn 0.
        Start() only sets bphaseinprogress - false, current phase to -1, and turn to 0. StartGame() than runs in phase update and check player input.

        Develop a function in the console which toggles the scrolling instead of a timer based system.
    */
    private void StartGame()
    {
        //Startgame requires rewrite as game phase and should toggle through a loop of strings which the player can progress using space bar.
        
        if(StartGamePhase == 0)
            {
                Console.PrintToConsole("Thank you for playing the first playable version River of Coin. \n ");
                Console.PrintToConsole("You can Press Space Bar to continue");
            }
        if(StartGamePhase == 1)
            {
                Console.PrintToConsole("This game is Katelyst's entry in the RNDGAME JAM II. \n \n");
            }
        if(StartGamePhase == 2)
            {
                Console.PrintToConsole("The River of Coin is home to a vast variety of traders, savy merchants, and customers. \n");
            }
         if(StartGamePhase == 3)
            {
                Console.PrintToConsole("Reeti Recently bought a shop with the ambition of ascending up the river of coin, You can help by managing some of the day to day tasks.\n");
            }
        if(StartGamePhase == 4)
            {
                Console.PrintToConsole("Please reach out with any comments, we are excited to hear what you think! \n The game is currently limited in functionality, but aims to depict some core ideas. \n ");
            }
        if(StartGamePhase == 5)
            {
                Console.PrintToConsole("The Game loop will Start in a few seconds.");
                PhaseTimer = 10;
                InitializePlayerResources();
                bPhaseInProgress = true;
            }       
        StartGamePhase++;
    }

    /*
    InitializeTradePhase Generates a trade offer and trade key, it then communicates the offer information to the player.
        TO-DO: transfer most code to trade manager.
        TO-DO: Generate multiple trade offers effectively
        TO-DO: Visualize Trade Offers Using Interface.
    */
    private void InitializeTradePhase()
    {
        Console.PrintToConsole("Time to trade! \n");
        Console.LogScrollSpeed = 3;
        OfferList = TradeManager.GenerateTotalTrades(CurrentTurn, PlayerResources);

        foreach(TradeOfferScriptableObject Offer in OfferList)
        {
            Console.PrintToConsole("The astute " + Offer.MerchantName + ", Requests a grand total of: " + Offer.ResourcesRequested.ResourceQuantity + ", of your " + Offer.ResourcesRequested.ResourceName + "\n");
            Console.PrintToConsole("In Exchange for their: " + Offer.ResourcesOffered.ResourceQuantity + " " + Offer.ResourcesOffered.ResourceName + "\n");
            Console.PrintToConsole("Too Agree with this offer, Press: " + Offer.TradeKey.ToString());
            print(Offer.TradeKey);
        }

        

    }


    /*
        ExecuteTradeTransaction is supposed to manage the transaction of resources between merchant and player 
        Currently something seems to go wrong with the resource quantity values. and the resource scriptable objects are overwriting eachother instead of modifying eachothers variables.
            TO-DO: Fix transaction system.
    */
    private void ExecuteTradeTransaction(TradeOfferScriptableObject LocalOffer)
    {
        print(LocalOffer);
        print(LocalOffer.ResourcesOffered.ResourceName + LocalOffer.ResourcesOffered.ResourceQuantity);
        print(LocalOffer.ResourcesRequested.ResourceName + LocalOffer.ResourcesRequested.ResourceQuantity); 

        List<ResourceScriptableObject> NewPlayerResources = LocalOffer.AcceptTradeOffer(PlayerResources);
        PlayerResources = NewPlayerResources;

        Console.PrintToConsole("You Accepted the merchants offer!" + "\n");
        PrintPlayerResources();

        TradeManager.DestroyCard(LocalOffer);
    }

    /*
        Initializes the End Phase which executes debt collection, and
        TO-DO: the final turn score-card communicates critical game information.
    */
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

    /*
    Initialize Contract Phase 
    TO-DO: generate new contract offers
    TO-DO: generate contract keys
    TO-DO: Display contract offer
    */
    private void InitializeContractPhase()
    {
        Console.PrintToConsole("New Contract Offers have Arrived. \n This phase is currently not functional");
    }

    /*
    Initialize Order Phase 
    TO-DO: generate new Order offers
    TO-DO: generate Order Keys
    TO-DO: Display Order Offers
    */
    private void InitializeOrderPhase()
    {
        Console.PrintToConsole("A few orders became available. \n This phase is currently not functional \n");
    }



}

