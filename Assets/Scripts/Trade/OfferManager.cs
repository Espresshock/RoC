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

    public List<OfferScriptableObject> GenerateTotalOffers(int turn, List<ResourceScriptableObject> PlayerResources)
    {
        
        Offers = new List<OfferScriptableObject>();
        
        //Generate Number of merchant instances to spawn
        int NumberOfMerchants = 2;
        
        for(int i = 0; i <= NumberOfMerchants; i++)
        {
            OfferScriptableObject Offer = GenerateOffer(turn, PlayerResources);
            //Generate 'Trade Offer' for each merchant instance
            Offers.Add(Offer);
            CardList.Add(Offer.GenerateCard(CardScriptReference, Offer, this.gameObject));
        }
        
        //return trade offers
        return Offers;
    }

    private OfferScriptableObject GenerateOffer(int turn, List<ResourceScriptableObject> PlayerResources)
    {        
        OfferScriptableObject Offer = ScriptableObject.CreateInstance<OfferScriptableObject>();

        Offer = Offer.CreateOffer(Offer, "", turn); 
        Offer.ResourcesRequested = Offer.GenerateResource(Offer.GenerateTradeQuality(turn));
        Offer.ResourcesOffered = Offer.GenerateResource(Offer.GenerateTradeQuality(turn));
        Offer.TradeKey = Offer.GenerateTradeKey();
        
        return Offer;
    }
}
