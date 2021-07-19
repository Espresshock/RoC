using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeOfferScriptableObject : ScriptableObject
{
 
    public string MerchantName;

    public enum TRADE_QUALITY {BAD, FAIR, GOOD, EXCELLENT};
    public ResourceScriptableObject ResourcesRequested;
    public ResourceScriptableObject ResourcesOffered;


    private TradeOfferScriptableObject InitTrade(string Name, TRADE_QUALITY Quality){
        this.MerchantName = NameMerchant(Name);
        this.SetTradeQuality(Quality);

        return this;
    }

    public TradeOfferScriptableObject CreateTradeOffer(TradeOfferScriptableObject TradeOffer, string Name,TRADE_QUALITY Quality){
        TradeOffer.InitTrade(Name, Quality);
        return TradeOffer;
    }
    
    private void SetTradeQuality(TRADE_QUALITY Quality)
    {

        switch(Quality){
            case TRADE_QUALITY.BAD:
                //High resource requested Cost, Low resource offered Reward

                break;
            case TRADE_QUALITY.FAIR:
                // Medium resource requested Cost, Medium resource offered Reward
                break;
            case TRADE_QUALITY.GOOD:
                // Low resource requested Cost, High resource offered Reward
                break;
            case TRADE_QUALITY.EXCELLENT:
                //Minimal resource requested Cost, Excelent resource offered Reward
                break;
        }
    }

    private string NameMerchant(string Name)
    {
        if(Name != ""){
            return name;
        }
        else{
            return "Merchant Goobers";
        }
    }


    // Generate Requested resource type   
    // Generate requested resource quantity
    // Generate offered resource type
    // Generate offered resource quantity.

    public void AcceptTradeOffer(){
        //When Player Accepts Trade Offer
        //Remove resources requested from player
        //Add Reources offered to player 
        //Print Trade Offer
    }
}