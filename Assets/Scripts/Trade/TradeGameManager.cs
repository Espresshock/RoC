using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeGameManager : MonoBehaviour
{
    private KeyCode TradeKey; //used to generate a randomised key for trade offers. May be combined for each mechanic and added to seperate input manager later.

    public Card CardScriptReference;

    private Card CardReference;
    
    private GameObject CardGameObject;

    private List<Card> CardList;

    private List<TradeOfferScriptableObject> Trades;

    // Start is called before the first frame update
    void Start()
    {
        CardList = new List<Card>();
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
        
        Trades = new List<TradeOfferScriptableObject>();
        
        //Generate Number of merchant instances to spawn
        int NumberOfMerchants = 2;
        
        for(int i = 0; i <= NumberOfMerchants; i++)
        {
            TradeOfferScriptableObject TradeOffer = GenerateTradeOffer(turn, PlayerResources);
            //Generate 'Trade Offer' for each merchant instance
            Trades.Add(TradeOffer);
            CardList.Add(GenerateTradeCard(TradeOffer));
        }
        
        //return trade offers
        return Trades;
    }

    /*
    Generates a single trade offer objects
    Player can directly trade a selection of 'resources' for a selection of other 'resources'
    */
    private TradeOfferScriptableObject GenerateTradeOffer(int turn, List<ResourceScriptableObject> PlayerResources)
    {        
        TradeOfferScriptableObject Offer = ScriptableObject.CreateInstance<TradeOfferScriptableObject>();

        Offer = Offer.CreateTradeOffer(Offer, "", turn); 
        Offer.ResourcesRequested = Offer.GenerateResource(Offer.GenerateTradeQuality(turn), true, PlayerResources);
        Offer.ResourcesOffered = Offer.GenerateResource(Offer.GenerateTradeQuality(turn), false, PlayerResources);
        Offer.TradeKey = GenerateTradeKey();
        
        return Offer;
    }

    private Card GenerateTradeCard(TradeOfferScriptableObject Offer)
    {
        Card card;
        card = Instantiate(CardScriptReference, transform.position, transform.rotation);
        card.transform.SetParent(this.transform, false);

        card = card.CreateCard(card, Offer);
        CardReference = card;
        CardGameObject = card.gameObject;
        return card;
    }

    public Card GetCard()
    {
        if(CardReference != null){
            return CardReference;
        }
        else
        {
            return null;
        }
    }

    public void DestroyRemainingCards()
    {
        foreach(Card Card in CardList)
        {
            Destroy(Card.gameObject);
        }
    }

    public void DestroyCard(TradeOfferScriptableObject Offer)
    {
        foreach(TradeOfferScriptableObject Trade in Trades)
        {
            foreach(Card card in CardList)
            {
                if(Offer.TradeKey == Trade.TradeKey)
                {
                    Destroy(card.gameObject);
                }
            }
            
        }

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

        foreach(TradeOfferScriptableObject Offer in Trades)
        {
            if(TradeKey == Offer.TradeKey)
            {
                TradeKey = KeyCode.V;
            }
        }

        return TradeKey;
    }



}
