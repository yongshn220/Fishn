using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPopupController : MonoBehaviour
{
    private PopupManager popupManager;
    private PopupType type = PopupType.BagPopup;
    private BagContent bagContent;
    private Button blockingButton;

    void Awake()
    {
        bagContent = GetComponentInChildren<BagContent>();
        blockingButton = GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
    }
    public void Start()
    {
        blockingButton.onClick.AddListener(OnBlockingPanelClick);
    }

    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
    }

#region Button Event
    public void OnBuyButtonClick(int id)
    {

    }

    // Outside of the current UI is clicked -> Close the current UI.
    private void OnBlockingPanelClick()
    {
        popupManager.ClosePopup(this.type);
    }
#endregion
}
