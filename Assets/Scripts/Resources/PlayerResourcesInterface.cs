using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesInterface : MonoBehaviour
{

    private GameObject GameManagerReference;

    private Text CoinText;
    private Text ScrapText;
    private Text ShinyText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitializeResourceInterface()
    {
        GameManagerReference = GetComponentInParent<CoreGameManager>().gameObject;

        foreach(Text TextObject in this.GetComponentsInChildren<Text>())
        {  
            if(TextObject.tag == "Coin")
            {
                    CoinText = TextObject;

            }
            else if(TextObject.tag == "Scrap")
            {
                    ScrapText = TextObject;

            }
            else if(TextObject.tag == "Shiny")
            {
                    ShinyText = TextObject;

            }
        }

    }

    public void UpdateResourceInterface(List<ResourceScriptableObject> PlayerResources)
    {
        foreach(ResourceScriptableObject Resource in PlayerResources)
        {
            if(Resource.ResourceName == "Coin")
            {
                    CoinText.text = Resource.ResourceQuantity.ToString();

            }
            else if(Resource.ResourceName == "Scrap")
            {
                    ScrapText.text = Resource.ResourceQuantity.ToString();

            }
            else if(Resource.ResourceName == "Shiny")
            {
                    ShinyText.text = Resource.ResourceQuantity.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
