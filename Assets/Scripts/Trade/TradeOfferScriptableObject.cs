using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeOfferScriptableObject : ScriptableObject
{

    public string MerchantName; //Name of merchant, displayed in trade offer.

    public enum TRADE_QUALITY { BAD, FAIR, GOOD, EXCELLENT }; //Quality of trade offer, helps generate resource type and quantity
    public ResourceScriptableObject ResourcesRequested; //Resource requested by merchant
    public ResourceScriptableObject ResourcesOffered;   // resource offered by merchant

    /*
        Initializes a new tradeofferobject
    */
    public TradeOfferScriptableObject CreateTradeOffer(TradeOfferScriptableObject TradeOffer, string Name, int turn)
    {
        TradeOffer.MerchantName = NameMerchant(Name);
        TradeOffer.GenerateTradeQuality(turn);
        return TradeOffer;
    }

    /*
        Names the merchant if a name is provided, or generates a random name if one is not
    */
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

    /*
        Randomly generates a trade quality based on the current turn
    */
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

    /*
        Randomly generates a trade resource based on quality, request/offer, and player resources
    */
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

    /*
        Randomly generates a resource quantity based on trade quality
    */
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

    /*
        Generates a requested resource for a trade offer.
    */
    private ResourceScriptableObject GenerateRequestedResource(TRADE_QUALITY Quality, ResourceScriptableObject Resource, List<ResourceScriptableObject> PlayerResources)
    {
        Resource = PlayerResources[UnityEngine.Random.Range(0, PlayerResources.Count)];
        Resource.ResourceQuantity = GenerateQuantity(Quality);
        return Resource;
    }

    /*
        Generates an offered resource for a trade offer.
    */
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

    /*
    Funcion responsible for managing the transaction which takes place when agreeing to a trade offer.
    ExecuteTradeTransaction is supposed to manage the transaction of resources between merchant and player 
    Currently something seems to go wrong with the resource quantity values. and the resource scriptable objects are overwriting eachother instead of modifying eachothers variables.
    TO-DO: Fix transaction system.
    */
   
    public List<ResourceScriptableObject> AcceptTradeOffer(List<ResourceScriptableObject> PlayerResources)
    {

        bool OfferTransactionCompleted = false;

        foreach (ResourceScriptableObject PlayerResource in PlayerResources)
        {
            
            //Remove resources requested from player
            if (PlayerResource.ResourceName == ResourcesRequested.ResourceName && PlayerResource.ResourceQuantity >= ResourcesRequested.ResourceQuantity)
            {
                PlayerResource.ResourceQuantity = PlayerResource.ResourceQuantity - ResourcesRequested.ResourceQuantity;
            }

            //Add Resources offered to player 
            if (PlayerResource.ResourceName == ResourcesOffered.ResourceName)
            {
                
                PlayerResource.ResourceQuantity = PlayerResource.ResourceQuantity + ResourcesOffered.ResourceQuantity;
                OfferTransactionCompleted = true;
            }

            //Adds resource to resource list if player does not have resource yet.
            if(ResourcesOffered.ResourceQuantity > 0 && OfferTransactionCompleted)
            {
                PlayerResources.Add(ResourcesOffered);
                break;
            }
        }

        return PlayerResources;
    }
}
