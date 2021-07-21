using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeGameManager : MonoBehaviour
{
    private KeyCode TradeKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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


    public TradeOfferScriptableObject GenerateTradeOffer(int turn, List<ResourceScriptableObject> PlayerResources)
    {        
        TradeOfferScriptableObject Offer = ScriptableObject.CreateInstance<TradeOfferScriptableObject>();

        Offer = Offer.CreateTradeOffer(Offer, "", turn); 
        Offer.ResourcesRequested = Offer.GenerateResource(Offer.GenerateTradeQuality(turn), true, PlayerResources);
        Offer.ResourcesOffered = Offer.GenerateResource(Offer.GenerateTradeQuality(turn), false, PlayerResources);
        
        return Offer;
    }

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
