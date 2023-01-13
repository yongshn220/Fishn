using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePopupController : MonoBehaviour
{
    StoreContent storeContent;

    void Awake()
    {
        storeContent = transform.GetComponentInChildren<StoreContent>();
    }


    public void Setup()
    {
    }
}
