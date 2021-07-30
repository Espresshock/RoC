using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesInterface : MonoBehaviour
{

    public GameObject GameManagerReference;

    private Text CoinText;
    private Text ScrapText;
    private Text ShinyText;


    // Start is called before the first frame update
    void Start()
    {
        GameManagerReference = GetComponentInParent<CoreGameManager>().gameObject;
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeResourceInterface()
    {
        
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

    private void PrintPlayerResources()
    {
        GameManagerReference.GetComponent<CoreGameManager>().ReetiConsole.PrintToConsole("We now have" + "\n");
        foreach (ResourceScriptableObject resource in GameManagerReference.GetComponent<CoreGameManager>().PlayerResources)
        {
            GameManagerReference.GetComponent<CoreGameManager>().ReetiConsole.PrintToConsole("A total of: " + resource.ResourceQuantity + " " + resource.ResourceName);
        }
    }
}
