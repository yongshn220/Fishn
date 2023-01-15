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
        gameData = GameManager.instance.dataManager.gameData;
        coralText.text = gameData.coral.ToString();
    }

    // public void OpenPopup()
    // {
    //     throw new System.NotImplementedException();
    // }

    // public void ClosePopup()
    // {
    //     throw new System.NotImplementedException();
    // }
#endregion
}
