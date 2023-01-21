using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CheckPopupController : MonoBehaviour, IPopup
{
    private Button blockingButton;
    private Button noButton;
    private Button yesButton;

    private bool isButtonPressed;
    private bool bUserDecision;

    void Awake()
    {
        blockingButton = transform.GetComponentInChildren<BlockingPanel>().GetComponent<Button>();
        noButton = transform.GetComponentInChildren<NoButton>().GetComponent<Button>();
        yesButton = transform.GetComponentInChildren<YesButton>().GetComponent<Button>();
    }

    void Start()
    {
        blockingButton.onClick.AddListener(OnBlockingPanelClick);
        noButton.onClick.AddListener(OnNoButtonClick);
        yesButton.onClick.AddListener(OnYesButtonClick);
    }

#region IPopup
    public void Setup(PopupManager popupManager)
    {
        
    }

    public void Enable()
    {
        
    }

    public void Disable()
    {
        
    }
#endregion

    public async UniTask<bool> WaitUserDecision()
    {
        await new WaitUntil(() => isButtonPressed);
        return bUserDecision;
    }

    private void OnBlockingPanelClick()
    {}

    private void OnNoButtonClick()
    {   
        bUserDecision = false;
        isButtonPressed = true;
    }

    private void OnYesButtonClick()
    {
        bUserDecision = true;
        isButtonPressed = true;
    }
}
