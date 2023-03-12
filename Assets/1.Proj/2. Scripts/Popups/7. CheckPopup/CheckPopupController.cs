using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CheckPopupController : MonoBehaviour, IPopup
{
    public enum Option {Buy, Sell, Kill};
    private Button blockingButton;
    private Button noButton;
    private Button yesButton;
    private TMP_Text checkText;

    private bool isButtonPressed;
    private bool bUserDecision;

    void Awake()
    {
        blockingButton = transform.GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
        noButton = transform.GetComponentInChildren<NoButton>()?.GetComponent<Button>();
        yesButton = transform.GetComponentInChildren<YesButton>()?.GetComponent<Button>();
        checkText = transform.GetComponentInChildren<CheckText>()?.GetComponent<TMP_Text>();
    }

    void Start()
    {
        blockingButton.onClick.AddListener(OnBlockingPanelClick);
        noButton.onClick.AddListener(OnNoButtonClick);
        yesButton.onClick.AddListener(OnYesButtonClick);
    }

#region IPopup
    public void Setup(PopupManager popupManager){}

    public void Enable(int option, string data)
    {
        if (option == (int) Option.Buy)
        {
            checkText.text = $"Do you want to buy following Item?\n\"{data}\""; return;
        }
        if (option == (int) Option.Sell)
        {
            checkText.text = $"Do you want to sell following item?\n\"{data}\""; return;
        }
    }

    public void Disable(){}
#endregion

    public async UniTask<bool> WaitUserDecision()
    {
        await new WaitUntil(() => isButtonPressed);
        isButtonPressed = false;
        return bUserDecision;
    }

#region Button event

    private void OnBlockingPanelClick(){}

    private void OnCheckButtonClick()
    {
        
    }

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
#endregion
}
