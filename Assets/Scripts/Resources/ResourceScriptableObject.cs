using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScriptableObject : ScriptableObject
{

    public string ResourceName;

    //private int ResourceValue;

    public enum RESOURCE {COIN, CRYSTAL, MATERIALS, METAL_NUGGET, CRYSTAL_FRAGMENT, SCRAP};
    public int ResourceQuantity;
    

    private void Init(RESOURCE Resource, int Quantity){
        NameResource(Resource);
        this.ResourceQuantity = Quantity;
    }

    public ResourceScriptableObject SetResource(RESOURCE Resource, int Quantity){
        ResourceScriptableObject Object = ScriptableObject.CreateInstance<ResourceScriptableObject>();
        Object.Init(Resource, Quantity);
        return Object;
    }
    
    RESOURCE NameResource(RESOURCE Resource)
    {
        switch(Resource){
            case RESOURCE.COIN:
                this.ResourceName = "Coin";
                break;
            case RESOURCE.CRYSTAL:
                this.ResourceName = "Crystal";
                break;
            case RESOURCE.MATERIALS:
                this.ResourceName = "Materials";
                break;
            case RESOURCE.METAL_NUGGET:
                this.ResourceName = "Metal Nugget";
                break;
            case RESOURCE.CRYSTAL_FRAGMENT:
                this.ResourceName = "Crystal Fragment";
                break;
            case RESOURCE.SCRAP:
                this.ResourceName = "Scrap";
                break;
        }
        return Resource;
    }
    


}
