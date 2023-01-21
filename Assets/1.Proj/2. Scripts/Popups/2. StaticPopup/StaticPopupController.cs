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
        coralText.text = GameManager.instance.dataManager.GetUserCoral().ToString();
    }

    public void Enable()
    {

    }

    public void Disable()
    {
        
    }
#endregion
}
