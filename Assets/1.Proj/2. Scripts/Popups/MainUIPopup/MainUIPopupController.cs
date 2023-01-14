using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIPopupController : MonoBehaviour
{
    private PopupManager popupManager;
    public Button storeButton;
    public Button bagButton;
    

    void Start()
    {
        storeButton.onClick.AddListener(OnStoreButtonClick);
        bagButton.onClick.AddListener(OnBagButtonClick);
    }
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
    }
    
    private void OnStoreButtonClick()
    {
        popupManager.OpenPopup(PopupType.StorePopup);
    }

    private void OnBagButtonClick()
    {
        popupManager.OpenPopup(PopupType.BagPopup);
    }
}
