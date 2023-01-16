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
    
    public PopupType currentType = PopupType.MainUIPopup;
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
        currentType = type;
        DisableUI(PopupType.MainUIPopup);
        EnableUI(type);
    }

    public void ClosePopup(PopupType type)
    {
        currentType = PopupType.MainUIPopup;
        EnableUI(PopupType.MainUIPopup);
        DisableUI(type);
    }

    private void EnableUI(PopupType type)
    {
        MonoBehaviour target = popups[(int) type] as MonoBehaviour;
        if (target)
        {
            // Make the popup visible and call its own Enable().
            CanvasGroup targetUI = target.GetComponent<CanvasGroup>();
            targetUI.alpha = 1f;
            targetUI.interactable = true;
            targetUI.blocksRaycasts = true;
            popups[(int) type].Enable(); 
        }
    }

    private void DisableUI(PopupType type)
    {
        MonoBehaviour target = popups[(int) type] as MonoBehaviour; 
        if (target)
        {
            // Make the popup unvisible and call its own Disable();
            CanvasGroup targetUI = target.GetComponent<CanvasGroup>();
            targetUI.alpha = 0f;
            targetUI.interactable = false;
            targetUI.blocksRaycasts = false;
            popups[(int) type].Disable();
        }
    
    }
    
    private void EnableUIs(PopupType[] types)
    {
        foreach (var type in types)
        {
            EnableUI(type);
        }
    }

    private void DisableUIs(PopupType[] types)
    {
        foreach (var type in types)
        {
            DisableUI(type);
        }
    }

    public void ChangeCameraView(CameraType type)
    {
        sceneManager.cameraManager.ChangeCameraView(type);
    }
}