using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Card : MonoBehaviour
{
    public Card ThisCard;
    public OfferScriptableObject ThisOffer;
    public Image CardImage;
    public Image RequestedResourceImage;
    public Image OfferedResourceImage;

    public Text RequestedQuantity;
    public Text OfferedQuantity;

    public Text TriggerTurns; // still requires function for generation and assignment of text.



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void inititalizeImages(Card card, ResourceScriptableObject Requested, ResourceScriptableObject Offered)
    {
        Texture2D CardTex = null;
        Texture2D RequestTex = null;
        Texture2D OfferTex = null;
        foreach(Image image in GetComponentsInChildren<Image>())
        {
            
           if(image.CompareTag("Card"))
           {    
               CardImage = image;           
               switch(GetComponentInParent<CoreGameManager>().GameStateMachine.CurrentPhase)
               {
                    case 0:
                        //Contract
                        CardTex = LoadPNG("Assets/Art Assets/Cards/Contract-Card.png");

                        break;
                   case 1:
                        //Trade
                         CardTex = LoadPNG("Assets/Art Assets/Cards/Trade-Card.png");
                   break;
                   case 2:
                        //Supply
                        CardTex = LoadPNG("Assets/Art Assets/Cards/Supply-Card.png");
                        
                   break;
                   case 3:
                        //End
                   break;
               }
               
               // Get Phase to determine card background
           }
           else if(image.CompareTag("ResourceRequest"))
           {
               RequestedResourceImage = image;
               RequestTex = Requested.ResourceIcon;
               //LoadPNG for request resource name.
           }
           else if(image.CompareTag("ResourceOffer"))
           {
               OfferedResourceImage = image;
               OfferTex = Offered.ResourceIcon;
               //LoadPNG for offer resource name.
           }
        }

        if(CardTex != null)
        {
            card.CardImage.GetComponent<Image>().sprite = Sprite.Create(CardTex, new Rect(0.0f, 0.0f, CardTex.width, CardTex.height), new Vector2(0.5f,0.5f), 180.0f);
            RequestedResourceImage.GetComponent<Image>().sprite = Sprite.Create(RequestTex, new Rect(0.0f, 0.0f, RequestTex.width, RequestTex.height), new Vector2(0.5f,0.5f), 16.0f);
            OfferedResourceImage.GetComponent<Image>().sprite = Sprite.Create(OfferTex, new Rect(0.0f, 0.0f, OfferTex.width, OfferTex.height), new Vector2(0.5f,0.5f), 16.0f);
        }
        else{
            print("Could not load texture sprite thingymajig");
        }

    }

    private void InitializeQuantityText(Card card, int Requested, int Offered)
    {
        foreach(Text Textslot in GetComponentsInChildren<Text>())
        {
            if(Textslot.CompareTag("ResourceRequest"))
            {
                card.RequestedQuantity = Textslot;
                card.RequestedQuantity.text = Requested.ToString();
            }
            else if(Textslot.CompareTag("ResourceOffer"))
            {
                card.OfferedQuantity = Textslot;
                card.OfferedQuantity.text = Offered.ToString();
            }
        }
    }

    public Card CreateCard(Card card, OfferScriptableObject Offer)
    {
        ThisCard = card;
        ThisOffer = Offer;
        inititalizeImages(card, Offer.ResourcesRequested, Offer.ResourcesOffered);
        InitializeQuantityText(card, Offer.ResourcesRequested.ResourceQuantity, Offer.ResourcesOffered.ResourceQuantity);
        return this;
    }

    public Card GetCard()
    {
        return this;
    }

    /*

    public void DestroyRemainingCards()
    {
        foreach(Card Card in CardList)
        {
            Destroy(Card.gameObject);
        }
    }
    */
    public void DestroyCard()
    {
        
        Destroy(this.gameObject);
    }

    

    private static Texture2D LoadPNG(string filePath) {
 
     Texture2D tex = null;
     byte[] fileData;
 
     if (File.Exists(filePath))     {
         fileData = File.ReadAllBytes(filePath);
         tex = new Texture2D(2, 2);
         tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
     }
     return tex;
 }

}
