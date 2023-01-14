using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PopupType
{
    MainUIPopup,
    StorePopup,
}

public class PopupManager : MonoBehaviour
{
    public StaticPopupController staticPopupController;
    public MainUIPopupController mainUIPopupController;
    public StorePopupController storePopupController;
    

    void Awake()
    {
        staticPopupController = GetComponentInChildren<StaticPopupController>();
        mainUIPopupController = GetComponentInChildren<MainUIPopupController>();
        storePopupController = GetComponentInChildren<StorePopupController>();
    }

    public void Setup()
    {
        staticPopupController.GetComponent<Canvas>().enabled = true;
        mainUIPopupController.GetComponent<Canvas>().enabled = true;
        storePopupController.GetComponent<Canvas>().enabled = false;

        staticPopupController.Setup(this);
        mainUIPopupController.Setup(this);
        storePopupController.Setup(this);
    }

    public void OpenPopup(PopupType type)
    {
        if (type == PopupType.MainUIPopup)
        {
        }

        if (type == PopupType.StorePopup)
        {
            mainUIPopupController.GetComponent<Canvas>().enabled = false;
            storePopupController.GetComponent<Canvas>().enabled = true;
        }
    }

    public void ClosePopup(PopupType type)
    {
        
    }
}
