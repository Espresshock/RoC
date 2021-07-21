using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeOfferScriptableObject : ScriptableObject
{

    public string MerchantName;

    public enum TRADE_QUALITY { BAD, FAIR, GOOD, EXCELLENT };
    public ResourceScriptableObject ResourcesRequested;
    public ResourceScriptableObject ResourcesOffered;


    private TradeOfferScriptableObject InitTrade(TradeOfferScriptableObject TradeOffer, string Name, int turn)
    {
        TradeOffer.MerchantName = NameMerchant(Name);
        TradeOffer.GenerateTradeQuality(turn);

        return TradeOffer;
    }


    public TradeOfferScriptableObject CreateTradeOffer(TradeOfferScriptableObject TradeOffer, string Name, int turn)
    {
        TradeOffer.InitTrade(TradeOffer, Name, turn);
        return TradeOffer;
    }


    private string NameMerchant(string Name)
    {
        if (Name != "")
        {
            return name;
        }
        else
        {
            return "Merchant Goobers";
        }
    }


    public TradeOfferScriptableObject.TRADE_QUALITY GenerateTradeQuality(int turn)
    {
        TradeOfferScriptableObject.TRADE_QUALITY Quality = TRADE_QUALITY.BAD;
        int QualityGenerator = UnityEngine.Random.Range(0, 10);

        if (turn < 3)
        {
            if (QualityGenerator < 7)
            {
                Quality = TRADE_QUALITY.BAD;
            }
            else
            {
                Quality = TRADE_QUALITY.FAIR;
            }
        }
        else if (turn >= 3 && turn < 10)
        {
            if (QualityGenerator < 4)
            {
                Quality = TRADE_QUALITY.BAD;
            }
            else if (QualityGenerator >= 4 && QualityGenerator < 8)
            {
                Quality = TRADE_QUALITY.FAIR;
            }
            else if (QualityGenerator > 8)
            {
                Quality = TRADE_QUALITY.GOOD;
            }
            else
            {
                Quality = TRADE_QUALITY.FAIR;
            }

        }

        return Quality;
    }

    public ResourceScriptableObject GenerateResource(TRADE_QUALITY Quality, bool Request, List<ResourceScriptableObject> PlayerResources)
    {
        ResourceScriptableObject Resource = ScriptableObject.CreateInstance<ResourceScriptableObject>();

        if (Request)
        {
            Resource = GenerateRequestedResource(Quality, Resource, PlayerResources);
        }
        else
        {
            Resource = GenerateOfferedResource(Quality, Resource);
        }

        return Resource;
    }

    private int GenerateQuantity(TRADE_QUALITY Quality)
    {
        int Quantity = 0;
        switch (Quality)
        {
            case TRADE_QUALITY.BAD:
                Quantity = UnityEngine.Random.Range(1, 1);
                break;
            case TRADE_QUALITY.FAIR:
                Quantity = UnityEngine.Random.Range(1, 2);
                break;
            case TRADE_QUALITY.GOOD:
                Quantity = UnityEngine.Random.Range(1, 3);
                break;
            case TRADE_QUALITY.EXCELLENT:
                Quantity = UnityEngine.Random.Range(2, 4);
                break;
        }

        return Quantity;
    }

    private ResourceScriptableObject GenerateRequestedResource(TRADE_QUALITY Quality, ResourceScriptableObject Resource, List<ResourceScriptableObject> PlayerResources)
    {
        Resource = PlayerResources[UnityEngine.Random.Range(0, PlayerResources.Count)];
        Resource.ResourceQuantity = GenerateQuantity(Quality);
        return Resource;
    }

    private ResourceScriptableObject GenerateOfferedResource(TRADE_QUALITY Quality, ResourceScriptableObject Resource)
    {
        ResourceScriptableObject.RESOURCE ResourceType;
        switch (Quality)
        {
            case TRADE_QUALITY.BAD:
                if (UnityEngine.Random.Range(0, 1) == 0)
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.SCRAP;
                }
                else
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.METAL_NUGGET;
                }
                Resource.SetResource(Resource, ResourceType, GenerateQuantity(Quality));
                break;
            case TRADE_QUALITY.FAIR:
                if (UnityEngine.Random.Range(0, 1) == 0)
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.CRYSTAL_FRAGMENT;
                }
                else
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.COIN;
                }
                Resource.SetResource(Resource, ResourceType, GenerateQuantity(Quality));
                break;
            case TRADE_QUALITY.GOOD:
                ResourceType = ResourceScriptableObject.RESOURCE.COIN;
                Resource.SetResource(Resource, ResourceType, GenerateQuantity(Quality));
                break;
            case TRADE_QUALITY.EXCELLENT:

                break;
        }
        return Resource;
    }
    // Generate Requested resource type   
    // Generate requested resource quantity
    // Generate offered resource type
    // Generate offered resource quantity.

    public List<ResourceScriptableObject> AcceptTradeOffer(List<ResourceScriptableObject> PlayerResources)
    {

        bool OfferTransactionCompleted = false;

        foreach (ResourceScriptableObject PlayerResource in PlayerResources)
        {
            if (PlayerResource.ResourceName == ResourcesRequested.ResourceName && PlayerResource.ResourceQuantity >= ResourcesRequested.ResourceQuantity)
            {
                //Remove resources requested from player
                PlayerResource.ResourceQuantity = PlayerResource.ResourceQuantity - ResourcesRequested.ResourceQuantity;
            }

            if (PlayerResource.ResourceName == ResourcesOffered.ResourceName)
            {
                //Add Resources offered to player 
                PlayerResource.ResourceQuantity = PlayerResource.ResourceQuantity + ResourcesOffered.ResourceQuantity;
                OfferTransactionCompleted = true;
            }

            if(ResourcesOffered.ResourceQuantity > 0 && OfferTransactionCompleted)
            {
                PlayerResources.Add(ResourcesOffered);
            }
        }

        return PlayerResources;
    }
}
