using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSceneManager : MonoBehaviour
{
    private StoreItem storeItem;
    private FishManager fishManager;
    private StoreScenePopupManager popupManager;

    void Awake()
    {
        storeItem = transform.GetComponent<StoreItem>();
        fishManager = transform.GetComponent<FishManager>();
        popupManager = transform.GetComponent<StoreScenePopupManager>();
    }

    void Start()
    {
        storeItem.Setup();
        fishManager.Setup(storeItem.entityDataList);
    }

    void OpenPopup(PopupType type)
    {
        popupManager.OpenPopup(type);
    }

    void ClosePopup(PopupType type)
    {
        popupManager.ClosePopup(type);
    }
}
