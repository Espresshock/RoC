using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferScriptableObject : ScriptableObject
{
    // public OfferManager OfferManager;
    public string MerchantName; //Name of merchant, displayed in trade offer.

    public int TriggerTurn; //Number of turns in which offer is 'completed'

    public Card CardReference;
    public GameObject CardObjectReference;

    public enum OFFER_QUALITY { BAD, FAIR, GOOD, EXCELLENT }; //Quality of trade offer, helps generate resource type and quantity
    public enum OFFER_TYPE { CONTRACT, TRADE, SUPPLY }; //Quality of trade offer, helps generate resource type and quantity
    public ResourceScriptableObject ResourcesRequested; //Resource requested by merchant
    public ResourceScriptableObject ResourcesOffered;   // resource offered by merchant

    public KeyCode TradeKey;
    // Start is called before the first frame update
    
    public OfferScriptableObject CreateOffer(OfferScriptableObject Offer, string Name, int turn)
    {
        Offer.MerchantName = NameMerchant(Name);
        Offer.GenerateOfferQuality(turn);
        return Offer;
    } 

    public OfferScriptableObject.OFFER_QUALITY GenerateOfferQuality(int turn)
    {
        OfferScriptableObject.OFFER_QUALITY Quality = OFFER_QUALITY.BAD;
        int QualityGenerator = UnityEngine.Random.Range(0, 10);

        if (turn < 3)
        {
            if (QualityGenerator < 5)
            {
                Quality = OFFER_QUALITY.BAD;
            }
            else
            {
                Quality = OFFER_QUALITY.FAIR;
            }
        }
        else if (turn >= 3 && turn < 10)
        {
            if (QualityGenerator < 3)
            {
                Quality = OFFER_QUALITY.BAD;
            }
            else if (QualityGenerator >= 3 && QualityGenerator < 7)
            {
                Quality = OFFER_QUALITY.FAIR;
            }
            else if (QualityGenerator > 7)
            {
                Quality = OFFER_QUALITY.GOOD;
            }
            else
            {
                Quality = OFFER_QUALITY.FAIR;
            }

        }

        return Quality;
    }
    
    public List<ResourceScriptableObject> AcceptOffer(List<ResourceScriptableObject> PlayerResources)
    {


        foreach (ResourceScriptableObject PlayerResource in PlayerResources)
        {
            
            //Remove resources requested from player
            if (PlayerResource.ResourceName == ResourcesRequested.ResourceName && PlayerResource.ResourceQuantity >= ResourcesRequested.ResourceQuantity)
            {
                PlayerResource.ResourceQuantity = PlayerResource.ResourceQuantity -= ResourcesRequested.ResourceQuantity;
            }

            //Add Resources offered to player 
            if (PlayerResource.ResourceName == ResourcesOffered.ResourceName)
            {
                
                PlayerResource.ResourceQuantity = PlayerResource.ResourceQuantity + ResourcesOffered.ResourceQuantity;
            }

            //Adds resource to resource list if player does not have resource yet.
        }

        return PlayerResources;
    }

    public KeyCode GenerateTradeKey()
    {
        List<OfferScriptableObject> Trades = new List<OfferScriptableObject>(); //OfferManager.GetOfferList();
        
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

        //Rewrite the selection of keycode by checking other offers first.

        return TradeKey;
    }

    public Card GenerateCard(Card CardScript, OfferScriptableObject Offer, GameObject OfferManager)
    {
        Card card;
        card = Instantiate(CardScript, OfferManager.transform.position, OfferManager.transform.rotation);
        card.transform.SetParent(OfferManager.transform, false);

        card = card.CreateCard(card, Offer);
        CardReference = card;
        CardObjectReference = card.gameObject;
        return card;
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

    private int SetTriggerTurn()
    {
        return 0;
    }

      /*
        Randomly generates a resource quantity based on trade quality
    */
    private int GenerateQuantity(OFFER_QUALITY Quality)
    {
        int Quantity = 0;
        switch (Quality)
        {
            case OFFER_QUALITY.BAD:
                Quantity = UnityEngine.Random.Range(1, 2);
                break;
            case OFFER_QUALITY.FAIR:
                Quantity = UnityEngine.Random.Range(2, 3);
                break;
            case OFFER_QUALITY.GOOD:
                Quantity = UnityEngine.Random.Range(3, 5);
                break;
            case OFFER_QUALITY.EXCELLENT:
                Quantity = UnityEngine.Random.Range(4, 6);
                break;
        }

        return Quantity;
    }


    public ResourceScriptableObject GenerateResource(OFFER_QUALITY Quality)
    {
        ResourceScriptableObject Resource = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        ResourceScriptableObject.RESOURCE ResourceType;
        switch (Quality)
        {
            case OFFER_QUALITY.BAD:
                if (UnityEngine.Random.Range(0, 2) >= 1)
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.SCRAP;
                }
                else
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.COIN;
                }
                Resource.SetResource(Resource, ResourceType, GenerateQuantity(Quality));
                break;
            case OFFER_QUALITY.FAIR:
                if (UnityEngine.Random.Range(0, 2) >= 1)
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.SHINY;
                }
                else
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.COIN;
                }
                Resource.SetResource(Resource, ResourceType, GenerateQuantity(Quality));
                break;
            case OFFER_QUALITY.GOOD:
               if (UnityEngine.Random.Range(0, 2) >= 1)
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.SHINY;
                }
                else
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.COIN;
                }
                Resource.SetResource(Resource, ResourceType, GenerateQuantity(Quality));
                break;
            case OFFER_QUALITY.EXCELLENT:
                if (UnityEngine.Random.Range(0, 2) >= 1)
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.SHINY;
                }
                else
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.SCRAP;
                }
                Resource.SetResource(Resource, ResourceType, GenerateQuantity(Quality));
                break;
        }
        return Resource;
    }




    /*
    
    private ResourceScriptableObject GenerateRequestedResource(TRADE_QUALITY Quality, ResourceScriptableObject Resource, List<ResourceScriptableObject> PlayerResources)
    {
        Resource = PlayerResources[UnityEngine.Random.Range(0, PlayerResources.Count)];
        Resource.ResourceQuantity = GenerateQuantity(Quality);
        return Resource;
    }

    
        Generates an offered resource for a trade offer.

    private ResourceScriptableObject GenerateResource(TRADE_QUALITY Quality, ResourceScriptableObject Resource)
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
                    ResourceType = ResourceScriptableObject.RESOURCE.COIN;
                }
                Resource.SetResource(Resource, ResourceType, GenerateQuantity(Quality));
                break;
            case TRADE_QUALITY.FAIR:
                if (UnityEngine.Random.Range(0, 1) == 0)
                {
                    ResourceType = ResourceScriptableObject.RESOURCE.SHINY;
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
    
    
    
    
    
    */
}
