using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScriptableObject : ScriptableObject
{

    public string ResourceName; //Name of resource

    //private int ResourceValue;

    public enum RESOURCE {COIN, CRYSTAL, MATERIALS, METAL_NUGGET, CRYSTAL_FRAGMENT, SCRAP}; // recource type
    public int ResourceQuantity;    // number of resources held by this object
    

    //Initializes scriptableobject
    private void Init(RESOURCE Resource, int Quantity){
        NameResource(Resource);
        ResourceQuantity = Quantity;
    }

    //Allows external actors to set values of attached object
    public ResourceScriptableObject SetResource(ResourceScriptableObject Object, RESOURCE Resource, int Quantity){
        Object.Init(Resource, Quantity);
        return Object;
    }
    
    //Sets resource name based on type
    public RESOURCE NameResource(RESOURCE Resource)
    {
        switch(Resource){
            case RESOURCE.COIN:
                ResourceName = "Coin";
                break;
            case RESOURCE.CRYSTAL:
                ResourceName = "Crystal";
                break;
            case RESOURCE.MATERIALS:
                ResourceName = "Materials";
                break;
            case RESOURCE.METAL_NUGGET:
                ResourceName = "Metal Nugget";
                break;
            case RESOURCE.CRYSTAL_FRAGMENT:
                ResourceName = "Crystal Fragment";
                break;
            case RESOURCE.SCRAP:
                ResourceName = "Scrap";
                break;
        }
        return Resource;
    }
}
