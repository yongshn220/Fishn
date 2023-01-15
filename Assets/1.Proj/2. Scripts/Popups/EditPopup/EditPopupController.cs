using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPopupController : MonoBehaviour, IPopup
{
    private PopupManager popupManager;
    
    private PopupType type = PopupType.EditPopup;
    private Button blockingButton;

    void Awake()
    {
        blockingButton = GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
    }

    void Start()
    {
        blockingButton.onClick.AddListener(OnBlockingPanelClick);
    }
#region IPopup
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
    }
#endregion

    // Outside of the current UI is clicked -> Close the current UI.
    private void OnBlockingPanelClick()
    {
        popupManager.ClosePopup(this.type);
    }
}
