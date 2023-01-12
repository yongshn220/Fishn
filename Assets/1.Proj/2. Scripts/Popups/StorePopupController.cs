using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePopupController : MonoBehaviour
{
    StoreSceneManager storeSceneManager;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Setup()
    {
        storeSceneManager = transform.GetComponentInParent<StoreSceneManager>();
    }
}
