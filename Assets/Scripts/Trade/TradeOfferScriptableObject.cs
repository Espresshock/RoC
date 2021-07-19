using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeOfferScriptableObject : ScriptableObject
{
 
    public string MerchantName;

    public enum TRADE_QUALITY {BAD, FAIR, GOOD, EXCELLENT};
    List<ResourceScriptableObject> TotalResourcesRequested = new List<ResourceScriptableObject>();
    List<ResourceScriptableObject> ResourcesOffered = new List<ResourceScriptableObject>();

    private void InitTrade(string Name, TRADE_QUALITY Quality){
        NameMerchant(Name);
        SetTradeQuality(Quality);

    }

    public TradeOfferScriptableObject CreateTradeOffer(string Name,TRADE_QUALITY Quality){
        TradeOfferScriptableObject TradeOffer = ScriptableObject.CreateInstance<TradeOfferScriptableObject>();
        TradeOffer.InitTrade(Name, Quality);
        return TradeOffer;
    }
    
    TRADE_QUALITY SetTradeQuality(TRADE_QUALITY Quality)
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
        return Quality;
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

    private List<ResourceScriptableObject> GenerateRequestedResources(List<ResourceScriptableObject.RESOURCE> RequestedResource, List<int> RequestedQuantities)
    {
        
        foreach(ResourceScriptableObject.RESOURCE Resource in RequestedResource)
        {
            ResourceScriptableObject resource = new ResourceScriptableObject();
            resource.CreateResource(RequestedResource[RequestedResource.IndexOf(Resource)], RequestedQuantities[RequestedResource.IndexOf(Resource)]);
            TotalResourcesRequested.Add(resource);
        }
        
        return TotalResourcesRequested;
    }   
    // Generate requested resource quantity
    // Generate offered resource type
    // Generate offered resource quantity.


}
