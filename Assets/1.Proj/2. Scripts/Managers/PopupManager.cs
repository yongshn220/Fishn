using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PopupType
{
    MainUIPopup,
    StorePopup,
    BagPopup,
}

public class PopupManager : MonoBehaviour
{
    private StaticPopupController staticPopupController;
    private MainUIPopupController mainUIPopupController;
    private StorePopupController storePopupController;
    private BagPopupController bagPopupController;
    

    void Awake()
    {
        staticPopupController = GetComponentInChildren<StaticPopupController>();
        mainUIPopupController = GetComponentInChildren<MainUIPopupController>();
        storePopupController = GetComponentInChildren<StorePopupController>();
        bagPopupController = GetComponentInChildren<BagPopupController>();
    }

    public void Setup()
    {
        EnableUI(staticPopupController.transform);
        EnableUI(mainUIPopupController.transform);
        DisableUI(storePopupController.transform);
        DisableUI(bagPopupController.transform);

        staticPopupController.Setup(this);
        mainUIPopupController.Setup(this);
        storePopupController.Setup(this);
        bagPopupController.Setup(this);
    }
    
    public void OpenPopup(PopupType type)
    {
        if (type == PopupType.MainUIPopup)
        {
        }

        if (type == PopupType.StorePopup)
        {
            DisableUI(mainUIPopupController.transform);
            EnableUI(storePopupController.transform);
            storePopupController.Select();
        }

        if (type == PopupType.BagPopup)
        {
            DisableUI(mainUIPopupController.transform);
            EnableUI(bagPopupController.transform);
        }
    }

    public void ClosePopup(PopupType type)
    {
        if (type == PopupType.StorePopup)
        {
            EnableUI(mainUIPopupController.transform);
            DisableUI(storePopupController.transform);
        }

        if (type == PopupType.BagPopup)
        {
            EnableUI(mainUIPopupController.transform);
            DisableUI(bagPopupController.transform);
        }
    }

    private void EnableUI(Transform target)
    {
        CanvasGroup targetUI = target.GetComponent<CanvasGroup>();
        targetUI.alpha = 1f;
        targetUI.interactable = true;
        targetUI.blocksRaycasts = true;
    }

    private void DisableUI(Transform target)
    {
        CanvasGroup targetUI = target.GetComponent<CanvasGroup>();
        targetUI.alpha = 0f;
        targetUI.interactable = false;
        targetUI.blocksRaycasts = false;
    }
}
