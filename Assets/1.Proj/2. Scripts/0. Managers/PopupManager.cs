using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PopupType
{
    StaticPopup,
    MainUIPopup,
    StorePopup,
    BagPopup,
    EditPopup,
}

public class PopupManager : MonoBehaviour
{
    private ViewSceneManager sceneManager;

    private IPopup[] popups;

    void Awake()
    {
        popups = new IPopup[Enum.GetValues(typeof(PopupType)).Length];
        popups[(int) PopupType.StaticPopup] = GetComponentInChildren<StaticPopupController>();
        popups[(int) PopupType.MainUIPopup] = GetComponentInChildren<MainUIPopupController>();
        popups[(int) PopupType.StorePopup] = GetComponentInChildren<StorePopupController>();
        popups[(int) PopupType.BagPopup] = GetComponentInChildren<BagPopupController>();
        popups[(int) PopupType.EditPopup] = GetComponentInChildren<EditPopupController>();
    }

    public void Setup(ViewSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;

        SetInitialVisibleStatus();

        foreach (IPopup popup in popups)
        {
            popup.Setup(this);
        }
    }

    private void SetInitialVisibleStatus()
    {
        EnableUIs(new PopupType[] {PopupType.StaticPopup, PopupType.MainUIPopup});
        DisableUIs(new PopupType[] {PopupType.StorePopup, PopupType.BagPopup, PopupType.EditPopup});
    }
    
    public void OpenPopup(PopupType type)
    {
        // DisableUI(PopupType.MainUIPopup);
        // EnableUI(type);
        
        if (type == PopupType.MainUIPopup)
        {
        }

        if (type == PopupType.StorePopup)
        {
            DisableUI(PopupType.MainUIPopup);
            EnableUI(PopupType.StorePopup);
        }

        if (type == PopupType.BagPopup)
        {
            DisableUI(PopupType.MainUIPopup);
            EnableUI(PopupType.BagPopup);
        }

        if (type == PopupType.EditPopup)
        {
            DisableUI(PopupType.MainUIPopup);
            EnableUI(PopupType.EditPopup);
        }
    }

    public void ClosePopup(PopupType type)
    {
        if (type == PopupType.StorePopup)
        {
            EnableUI(PopupType.MainUIPopup);
            DisableUI(PopupType.StorePopup);
        }

        if (type == PopupType.BagPopup)
        {
            EnableUI(PopupType.MainUIPopup);
            DisableUI(PopupType.BagPopup);
        }
    }

    private void EnableUI(PopupType type)
    {
        GameObject target = (object) popups[(int) type] as GameObject;
        if (target)
        {
            CanvasGroup targetUI = target.GetComponent<CanvasGroup>();
            targetUI.alpha = 1f;
            targetUI.interactable = true;
            targetUI.blocksRaycasts = true;
        }
    }

    private void EnableUIs(PopupType[] types)
    {
        foreach (var type in types)
        {
            EnableUI(type);
        }
    }

    private void DisableUI(PopupType type)
    {
        GameObject target = (object) popups[(int) type] as GameObject;
        if (target)
        {
            CanvasGroup targetUI = target.GetComponent<CanvasGroup>();
            targetUI.alpha = 0f;
            targetUI.interactable = false;
            targetUI.blocksRaycasts = false;
        }
    }

    private void DisableUIs(PopupType[] types)
    {
        foreach (var type in types)
        {
            DisableUI(type);
        }
    }
}
