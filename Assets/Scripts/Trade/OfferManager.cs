using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferManager : MonoBehaviour
{

    public Card CardScriptReference;

    private List<Card> CardList;

    private List<OfferScriptableObject> Offers;
    // Start is called before the first frame update
    void Start()
    {
        CardList = new List<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Card> GetCardList()
    {
        return CardList;
    }

    public List<OfferScriptableObject> GenerateTotalOffers(int turn)
    {
        
        Offers = new List<OfferScriptableObject>();
        
        //Generate Number of merchant instances to spawn
        int NumberOfMerchants = 2;
        
        for(int i = 0; i <= NumberOfMerchants; i++)
        {
            OfferScriptableObject Offer = Offer = GenerateOffer(turn);
            //Generate 'Trade Offer' for each merchant instance
            Offers.Add(Offer);
            GetComponentInParent<CoreGameManager>().AudioManager.PlayCardSound();
        }
        
        //return trade offers
        return Offers;
    }

    private OfferScriptableObject GenerateOffer(int turn)
    {        
        OfferScriptableObject Offer = ScriptableObject.CreateInstance<OfferScriptableObject>();
        Offer = Offer.CreateOffer(Offer, "", turn); 

        OfferScriptableObject.OFFER_QUALITY Quality = Offer.GenerateOfferQuality(turn);
        ResourceScriptableObject Request = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        Request = Offer.GenerateResource(Quality);

        ResourceScriptableObject offer = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        offer = Offer.GenerateResource(Quality);
        if(Request.ResourceQuantity == offer.ResourceQuantity)
        {
            offer.ResourceQuantity = offer.ResourceQuantity + 1;
        }

        Offer.ResourcesRequested = Request;
        Offer.ResourcesOffered = offer;
        
        Offer.TradeKey = Offer.GenerateTradeKey();

        Card card;
        card = Offer.GenerateCard(CardScriptReference, Offer, this.gameObject);
        CardList.Add(card);
        
        return Offer;
    }
}
