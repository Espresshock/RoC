using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public OfferManager OfferManager; //Reference to the trade manager component. Handles generation of trade offers and supporting elements

    public List<float> PhaseDurationArray = new List<float>(); //Publicly adjustable list of the number and duration of phases.

    public CoreGameManager GameManagerReference;
    public int CurrentPhase; // current player phase

    public int CurrentTurn; // current player turn.
    public List<OfferScriptableObject> OfferList;

    private OfferScriptableObject Offer; //Reference to current trade offer
    // Start is called before the first frame update
    public bool bPhaseInProgress; //Checks whether phase is currently progressing.
    
    private float PhaseTimer; //Tracks time of phase.

    public Console ConsoleReference;
    

    public int StartGamePhase;

    
    void Start()
    {
        bPhaseInProgress = false;
        CurrentPhase = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PhaseUpdate()
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
        Ends the current phase and executes any functions which have to be fired at the end of a phase.
    */
    public void EndCurrentPhase()
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

    private void ResolveCurrentPhase()
    {
        if(OfferList != null)
        {
            GameManagerReference.ResourceInterfaceReference.UpdateResourceInterface(GameManagerReference.PlayerResources);
            if(OfferManager.GetCardList().Count > 0)
            {
                foreach(Card card in OfferManager.GetCardList())
                {
                    OfferManager.GetCardList().Remove(card);
                    card.DestroyCard();
                }
            }

            // Destroy cards. TradeManager.DestroyRemainingCards();
        }
    }
    private void EndCurrentTurn()
    {
        CurrentPhase = 0;
        ConsoleReference.PrintToConsole("End of turn: " + CurrentTurn);
        CurrentTurn += 1;
        StartPhase(CurrentPhase);
    }

    private void StartPhase(int phase)
    {
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
        Initializes the player with a certain amount of resources when the game starts.
    */
    private void InitializePlayerResources()
    {

        ResourceScriptableObject StartCoin = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        StartCoin.SetResource(StartCoin, ResourceScriptableObject.RESOURCE.COIN, 20);
        GameManagerReference.PlayerResources.Add(StartCoin);

        ResourceScriptableObject StartScrap = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        StartCoin.SetResource(StartScrap, ResourceScriptableObject.RESOURCE.SCRAP, 5);
        GameManagerReference.PlayerResources.Add(StartScrap);

        ResourceScriptableObject StartShiny = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        StartCoin.SetResource(StartShiny, ResourceScriptableObject.RESOURCE.SHINY, 2);
        GameManagerReference.PlayerResources.Add(StartShiny);

        if(GameManagerReference.ResourceInterfaceReference == null)
        {
            GameManagerReference.ResourceInterfaceReference = GameManagerReference.GetComponentInChildren<PlayerResourcesInterface>();
        }
        GameManagerReference.ResourceInterfaceReference.InitializeResourceInterface();
        GameManagerReference.ResourceInterfaceReference.UpdateResourceInterface(GameManagerReference.PlayerResources);

    }

    /*
    InitializeTradePhase Generates a trade offer and trade key, it then communicates the offer information to the player.
        TO-DO: transfer most code to trade manager.
        TO-DO: Generate multiple trade offers effectively
        TO-DO: Visualize Trade Offers Using Interface.
    */
    private void InitializeTradePhase()
    {
        ConsoleReference.PrintToConsole("Time to trade! \n");
        OfferList = OfferManager.GenerateTotalOffers(CurrentTurn);

        foreach(OfferScriptableObject Offer in OfferList)
        {
           // ConsoleReference.PrintToConsole("Merchant Requests a total of: " + Offer.ResourcesRequested.ResourceQuantity + ", of your " + Offer.ResourcesRequested.ResourceName + "\n");
           //ConsoleReference.PrintToConsole("This in Exchange for their: " + Offer.ResourcesOffered.ResourceQuantity + " " + Offer.ResourcesOffered.ResourceName + "\n");
            ConsoleReference.PrintToConsole("Too Agree with this offer, Press: " + Offer.TradeKey.ToString());
            //print(Offer.TradeKey);
        }

        

    }

    /*
        ExecuteTradeTransaction is supposed to manage the transaction of resources between merchant and player 
        Currently something seems to go wrong with the resource quantity values. and the resource scriptable objects are overwriting eachother instead of modifying eachothers variables.
            TO-DO: Fix transaction system.
    */
    public void ExecuteTradeTransaction(OfferScriptableObject LocalOffer)
    {
        print(LocalOffer);
        print(LocalOffer.ResourcesOffered.ResourceName + LocalOffer.ResourcesOffered.ResourceQuantity);
        print(LocalOffer.ResourcesRequested.ResourceName + LocalOffer.ResourcesRequested.ResourceQuantity); 

        List<ResourceScriptableObject> NewPlayerResources = LocalOffer.AcceptOffer(GameManagerReference.PlayerResources);
        GameManagerReference.PlayerResources = NewPlayerResources;

        ConsoleReference.PrintToConsole("You Accepted the merchants offer!" + "\n");

        
        OfferManager.GetCardList().Remove(LocalOffer.CardReference.GetCard());
        LocalOffer.CardReference.GetCard().DestroyCard();

        GameManagerReference.ResourceInterfaceReference.UpdateResourceInterface(GameManagerReference.PlayerResources);
    }

/*
        Initializes the End Phase which executes debt collection, and
        TO-DO: the final turn score-card communicates critical game information.
    */
private void InitializeEndOfDay()
    {
        ConsoleReference.PrintToConsole("The Day has come to an end, The debt collector takes his cut \n");
        foreach (ResourceScriptableObject resource in GameManagerReference.PlayerResources)
        {
            int debt = 1;
            if (resource.ResourceName == "Coin")
            {
                resource.ResourceQuantity = resource.ResourceQuantity - debt;
                ConsoleReference.PrintToConsole("He takes: " + debt + " " + resource.ResourceName + " As his payment. \n");
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
        ConsoleReference.PrintToConsole("New Contract Offers have Arrived. \n This phase is currently not functional");
    }

    
    /*
    Initialize Order Phase 
    TO-DO: generate new Order offers
    TO-DO: generate Order Keys
    TO-DO: Display Order Offers
    */
    private void InitializeOrderPhase()
    {
        ConsoleReference.PrintToConsole("A few orders became available. \n This phase is currently not functional \n");
    }

    /*
        Ends the current turn and resets the phase.
        // TO-DO: ResolveTurn
        // TO-DO: Feedback / Score Card
        // Startt phase 0 of next turn.
    */


    public void StartGame()
    {
        //Startgame requires rewrite as game phase and should toggle through a loop of strings which the player can progress using space bar.
        
        if(StartGamePhase == 0)
            {
                ConsoleReference.PrintToConsole("Thank you for playing River of Coin v0.2.5 \n ");
                ConsoleReference.PrintToConsole("You can Press Space Bar to continue");
            }
        if(StartGamePhase == 1)
            {
                ConsoleReference.PrintToConsole("This game is Katelyst's entry in the RNDGAME JAM II. \n \n");
            }
        if(StartGamePhase == 2)
            {
                ConsoleReference.PrintToConsole("The River of Coin is home to a vast variety of traders, savy merchants, and customers. \n");
            }
         if(StartGamePhase == 3)
            {
                ConsoleReference.PrintToConsole("Reeti Recently bought a shop with the ambition of ascending up the river of coin, You can help by managing some of the day to day tasks.\n");
            }
        if(StartGamePhase == 4)
            {
                ConsoleReference.PrintToConsole("Please reach out with any comments, we are excited to hear what you think! \n The game is currently limited in functionality, but aims to depict some core ideas. \n ");
            }
        if(StartGamePhase == 5)
            {
                ConsoleReference.PrintToConsole("The Game loop will Start in a few seconds. \n Use z, x, and c to accept different trades!");
                PhaseTimer = 10;
                InitializePlayerResources();
                bPhaseInProgress = true;
                CurrentPhase = 0;
            }       
        StartGamePhase++;
    }

     /*
    Phase Update Manages the progression of phases .
    */
    

    /*
    EndCurrentPhase Communicates a phase has ended, 
    determines whether to start a next phase, 
    or to end the turn and reset phases.
    */
    

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
    

    
    

    
    

    /*
    If a phase is in progress, check player input waits to receive any of the phase valid input options.
    At all times, check player input waits to receive space bar input to end the current phase.
    Check player input may be transfered to a seperate object which will handle this gameplay, along with any control assignment functions.
    */
    

    /*
        Prints the contents of the playerResources list to the console
    */
    

    


    /*
        The start sequence should run an event which introduces the player to the world and the goal.
        Currently this event is a timer based prompt, user would like this to become button press event. Change StartGame function to phase -1 in turn 0.
        Start() only sets bphaseinprogress - false, current phase to -1, and turn to 0. StartGame() than runs in phase update and check player input.

        Develop a function in the console which toggles the scrolling instead of a timer based system.
    */
}
