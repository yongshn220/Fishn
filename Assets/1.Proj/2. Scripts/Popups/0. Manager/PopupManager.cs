using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PopupType
{
    StaticPopup,
    MainUIPopup,
    StorePopup,
    BagPopup,
    EditPopup,
    CheckPopup,
    InfoPopup,
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
        popups[(int) PopupType.CheckPopup] = GetComponentInChildren<CheckPopupController>();
        popups[(int) PopupType.InfoPopup] = GetComponentInChildren<InfoPopupController>();
    }

#region Set up
    public void Setup(ViewSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;

        foreach (IPopup popup in popups)
        {
            popup.Setup(this);
        }

        SetInitialVisibility(); // All popups must be set up first.
    }

    private void SetInitialVisibility()
    {
        EnableUIs(new PopupType[] {PopupType.StaticPopup, PopupType.MainUIPopup});
        DisableUIs(new PopupType[] {PopupType.StorePopup, PopupType.BagPopup, PopupType.EditPopup, PopupType.CheckPopup, PopupType.InfoPopup});
    }
#endregion

#region On / Off
    public void OpenPopup(PopupType type)
    {
        print("open popup");
        DisableAll();
        EnableUI(type);
    }

    public void ClosePopup(PopupType type)
    {
        DisableUI(type);
        EnableUI(PopupType.MainUIPopup);
    }

    private void EnableUI(PopupType type, int option = 0, string data = "")
    {
        currentType = type;
        print("type changed : " + currentType);
        MonoBehaviour target = popups[(int) type] as MonoBehaviour;
        if (target)
        {
            // Make the popup visible and call its own Enable().
            CanvasGroup targetUI = target.GetComponent<CanvasGroup>();
            targetUI.alpha = 1f;
            targetUI.interactable = true;
            targetUI.blocksRaycasts = true;
            popups[(int) type].Enable(option, data); 
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

    private void DisableAll()
    {
        foreach (var type in Enum.GetValues(typeof(PopupType)))
        {
            if ((PopupType)type == PopupType.StaticPopup) continue;
            DisableUI((PopupType) type);
        }
    }

    public void ChangeCameraView(CameraType type)
    {
        sceneManager.cameraManager.ChangeCameraView(type);
    }
#endregion

#region Edit Popup Interaction
    public void UpdateAllSeaObjectPosition()
    {
        sceneManager.fishTankManager.UpdateAllSeaObjectMonoPosition();
    }

    public void UpdateAllCoralPlantPosition()
    {
        sceneManager.fishTankManager.UpdateAllCoralPlantPosition();
    }

    public RectTransform GetRectTransform()
    {
        return transform.GetComponent<RectTransform>();
    }

    public void RemoveSeaObjectFromTank(SeaObjectMono seaObjectMono)
    {   
        sceneManager.fishTankManager.RemoveSeaObjectFromTank(seaObjectMono);
    }

    public void RemoveCoralPlantFromTank(CoralPlantMono coralPlantMono)
    {
        sceneManager.fishTankManager.RemoveCoralPlantFromTank(coralPlantMono);
    }

    public void SetCoralLightingState(bool state)
    {
        sceneManager.fishTankManager.SetCoralLightingState(state);
    }
#endregion

#region Store Popup Interaction
    public async UniTaskVoid TryBuyItem(int id, ItemType type, int coral, string name)
    {
        EnableUI(PopupType.CheckPopup, (int) CheckPopupController.Option.Buy, name);

        CheckPopupController checkPopup = popups[(int) PopupType.CheckPopup] as CheckPopupController;

        bool bUserDecision = await checkPopup.WaitUserDecision();

        if (bUserDecision) 
        {
            bool result = await GameManager.instance.purchaseManager.TryPurchase(id, type, coral);
        }
        DisableUI(PopupType.CheckPopup);
    }
#endregion

#region Info Popup Interaction
    public async UniTaskVoid TrySellItem(EntityMono entityMono)
    {
        EnableUI(PopupType.CheckPopup, (int) CheckPopupController.Option.Sell, entityMono.name); // Overlap CheckPopup on current Popup;

        CheckPopupController checkPopup = popups[(int) PopupType.CheckPopup] as CheckPopupController;

        bool bUserDecision = await checkPopup.WaitUserDecision();

        if (bUserDecision) 
        {
            bool result = await GameManager.instance.purchaseManager.TrySellEntity(entityMono.id, ItemType.Entity, entityMono.coral);
        }

        DisableUI(PopupType.CheckPopup); // Close overlapped checkpoup.
        OpenPopup(PopupType.MainUIPopup); // goto Main.
    }
#endregion

#region Bag Popup Interaction
    public List<SeaObjectData> GetDisabledSeaObjectDataList()
    {
        return sceneManager.fishTankManager.GetDisabledSeaObjectDataList();
    }

    public List<CoralPlantData> GetDisabledCoralPlantDataList()
    {
        return sceneManager.fishTankManager.GetDisabledCoralPlantDataList();
    }

    public void LoadSeaObjectFromBag(int type_id)
    {
        sceneManager.fishTankManager.LoadSeaObjectFromBag(type_id);
    }

    public void LoadCoralPlantFromBag(int type_id)
    {
        sceneManager.fishTankManager.LoadCoralPlantFromBag(type_id);
    }
#endregion
}
