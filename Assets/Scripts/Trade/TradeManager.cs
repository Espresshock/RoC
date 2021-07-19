using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private List<TradeOfferScriptableObject> GenerateTotalTrades(int CoinBalance,Hashtable Resources)
    {
        
        List<TradeOfferScriptableObject> Trades = new List<TradeOfferScriptableObject>();
        
        //Generate Number of merchant instances to spawn
        int NumberOfMerchants= Random.Range(1,3);
        
        for(int i = 0; i <= NumberOfMerchants; i++)
        {
            //Generate 'Trade Offer' for each merchant instance
            Trades.Add(GenerateTradeOffer());
        }
        
        //return trade offers
        return Trades;
    }


    private TradeOfferScriptableObject GenerateTradeOffer()
    {
        TradeOfferScriptableObject Offer = new TradeOfferScriptableObject();
        Offer.CreateTradeOffer("",TradeOfferScriptableObject.TRADE_QUALITY.BAD);
        return Offer;
    }



}
