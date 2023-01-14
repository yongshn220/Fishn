using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticPopupController : MonoBehaviour
{
    PopupManager popupManager;

    public TMP_Text coralText;
    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        gameData = GameManager.instance.dataManager.gameData;
        coralText.text = gameData.coral.ToString();
    }
}
