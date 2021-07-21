using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeGameManager : MonoBehaviour
{
    private KeyCode TradeKey; //used to generate a randomised key for trade offers. May be combined for each mechanic and added to seperate input manager later.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
        Generates a number of trade offers depending on the number of merchants that spawned this turn
    */
    public List<TradeOfferScriptableObject> GenerateTotalTrades(int turn, List<ResourceScriptableObject> PlayerResources)
    {
        
        List<TradeOfferScriptableObject> Trades = new List<TradeOfferScriptableObject>();
        
        //Generate Number of merchant instances to spawn
        int NumberOfMerchants= Random.Range(1,3);
        
        for(int i = 0; i <= NumberOfMerchants; i++)
        {
            //Generate 'Trade Offer' for each merchant instance
            Trades.Add(GenerateTradeOffer(turn, PlayerResources));
        }
        
        //return trade offers
        return Trades;
    }

    /*
    Generates a single trade offer objects
    Player can directly trade a selection of 'resources' for a selection of other 'resources'
    */
    public TradeOfferScriptableObject GenerateTradeOffer(int turn, List<ResourceScriptableObject> PlayerResources)
    {        
        TradeOfferScriptableObject Offer = ScriptableObject.CreateInstance<TradeOfferScriptableObject>();

        Offer = Offer.CreateTradeOffer(Offer, "", turn); 
        Offer.ResourcesRequested = Offer.GenerateResource(Offer.GenerateTradeQuality(turn), true, PlayerResources);
        Offer.ResourcesOffered = Offer.GenerateResource(Offer.GenerateTradeQuality(turn), false, PlayerResources);
        
        return Offer;
    }

    //used to generate a randomised key for trade offers. May be combined for each mechanic and added to seperate input manager later.
    public KeyCode GenerateTradeKey()
    {

        switch(Random.Range(0,2))
        {
            case 0:
                TradeKey = KeyCode.Z;
                break;
            case 1:
                TradeKey = KeyCode.X;
                break;
            case 2:
                TradeKey = KeyCode.C;
                break;
        }

        return TradeKey;
    }



}
