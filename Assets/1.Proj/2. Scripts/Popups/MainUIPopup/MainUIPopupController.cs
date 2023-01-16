using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIPopupController : MonoBehaviour, IPopup
{
    private PopupManager popupManager;
    private Button storeButton;
    private Button bagButton;
    private Button editButton;

    void Awake()
    {
        storeButton = GetComponentInChildren<MainUIStoreButton>()?.GetComponent<Button>();
        bagButton = GetComponentInChildren<MainUIBagButton>()?.GetComponent<Button>();
        editButton = GetComponentInChildren<MainUIEditButton>()?.GetComponent<Button>();
    }

    void Start()
    {
        storeButton.onClick.AddListener(OnStoreButtonClick);
        bagButton.onClick.AddListener(OnBagButtonClick);
        editButton.onClick.AddListener(OnEditButtonClick);
    }

#region IPopup
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
    }
#endregion

    private void OnStoreButtonClick()
    {
        print("A");
        popupManager.OpenPopup(PopupType.StorePopup);
    }

    private void OnBagButtonClick()
    {
        popupManager.OpenPopup(PopupType.BagPopup);
    }

    private void OnEditButtonClick()
    {
        popupManager.ChangeCameraView(CameraType.EditFrontCamera);
        popupManager.OpenPopup(PopupType.EditPopup);
    }
}
