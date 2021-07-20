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


    public List<TradeOfferScriptableObject> GenerateTotalTrades()
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


    public TradeOfferScriptableObject GenerateTradeOffer()
    {
        ResourceScriptableObject RequestedResource = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        ResourceScriptableObject OfferedResource = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        

        TradeOfferScriptableObject Offer = ScriptableObject.CreateInstance<TradeOfferScriptableObject>();
        Offer.CreateTradeOffer(Offer, "", TradeOfferScriptableObject.TRADE_QUALITY.BAD);
        Offer.ResourcesRequested = RequestedResource.SetResource(RequestedResource, ResourceScriptableObject.RESOURCE.COIN, 1);
        Offer.ResourcesOffered = OfferedResource.SetResource(RequestedResource, ResourceScriptableObject.RESOURCE.SCRAP, 1);
        return Offer;
    }



}
