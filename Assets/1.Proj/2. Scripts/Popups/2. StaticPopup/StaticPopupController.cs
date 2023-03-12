using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticPopupController : MonoBehaviour, IPopup
{
    PopupManager popupManager;

    public TMP_Text coralText;
    private GameData gameData;

#region IPopup
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        DelegateManager.OnCoralUpdate += OnCoralUpdate;
        
        coralText.text = Wallet.coral.ToString();
    }

    public void Enable(int option, string data){}

    public void Disable(){}
#endregion

#region Action Callback
    private void OnCoralUpdate()
    {
        coralText.text = Wallet.coral.ToString();
    }
#endregion
}
