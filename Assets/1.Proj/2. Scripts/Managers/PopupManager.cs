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
    public MainUIPopupController mainUIPopupController;
    public StorePopupController storePopupController;

    void Awake()
    {
        mainUIPopupController = GetComponentInChildren<MainUIPopupController>();
        storePopupController = GetComponentInChildren<StorePopupController>();
    }

    public void Setup()
    {
        // mainUIPopupController.gameObject.SetActive(false);
        storePopupController.gameObject.SetActive(false);
    }

    public void OpenPopup(PopupType type)
    {
        if (type == PopupType.MainUIPopup)
        {

        }

        if (type == PopupType.StorePopup)
        {
            storePopupController.gameObject.SetActive(true);
            storePopupController.Setup();
        }
    }
}
