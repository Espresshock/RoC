using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourceScriptableObject : ScriptableObject
{

    public string ResourceName; //Name of resource
    public Sprite ResourceIcon;

    //private int ResourceValue;

    public enum RESOURCE {COIN, SCRAP, SHINY}; // recource type
    public int ResourceQuantity;    // number of resources held by this object
    

    //Initializes scriptableobject
    private void Init(RESOURCE Resource, int Quantity){
        NameResource(Resource);
        ResourceQuantity = Quantity;
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

    //Allows external actors to set values of attached object
    public ResourceScriptableObject SetResource(ResourceScriptableObject Object, RESOURCE Resource, int Quantity){
        Object.Init(Resource, Quantity);
        return Object;
    }
    
    //Sets resource name based on type
    public RESOURCE NameResource(RESOURCE Resource)
    {
        Sprite IconTemp;
        switch(Resource){
            case RESOURCE.COIN:
                ResourceName = "Coin";
                IconTemp = Resources.Load<Sprite>("ResourceArt/material_122");
                //IconTemp = LoadPNG("Assets/Resources/ResourceArt/material_122.png");
                ResourceIcon = IconTemp;
                break;
            case RESOURCE.SCRAP:
                ResourceName = "Scrap";
                IconTemp = Resources.Load<Sprite>("ResourceArt/material_11");
                //IconTemp = LoadPNG("Assets/Resources/ResourceArt/material_122.png");
                ResourceIcon = IconTemp;
                break;
            case RESOURCE.SHINY:
                ResourceName = "Shiny";
                IconTemp = ResourceIcon = Resources.Load<Sprite>("ResourceArt/material_08");
                break;
        }
        return Resource;
    }
}
