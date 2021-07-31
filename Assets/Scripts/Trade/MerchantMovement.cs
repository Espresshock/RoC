using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject PointA;
    public GameObject PointB;

    public GameObject MerchantObject;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveMerchant()
    {
        if(MerchantObject.transform != PointA.transform)
        {
            while(MerchantObject.transform != PointA.transform)
            {
                MerchantObject.transform.position.Set(MerchantObject.transform.position.x -1, MerchantObject.transform.position.y, MerchantObject.transform.position.z);
            }
        }
        else if(MerchantObject.transform != PointB.transform)
        {
            while(MerchantObject.transform != PointB.transform)
            {
                MerchantObject.transform.position.Set(MerchantObject.transform.position.x +1, MerchantObject.transform.position.y, MerchantObject.transform.position.z);
            }
        }
    }
}
