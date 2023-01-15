using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSceneManager : MonoBehaviour
{
    FishTankManager fishTankManager;
    FishManager fishManager;
    PopupManager popupManager;

    List<FishData> fishDataList;
    GameData gameData;

    void Awake()
    {
        fishTankManager = GetComponentInChildren<FishTankManager>();
        fishManager = GetComponentInChildren<FishManager>();
        popupManager = GetComponentInChildren<PopupManager>();
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(!GameManager.instance.dataManager.isDataReady)
        {
            yield return new WaitForSeconds(1);
            //Wait and Try again
        }
        fishDataList = LoadFishDataList();
        gameData = LoadGameData();

        Setup();
    }

    private void Setup()
    {
        fishTankManager.Setup(this, gameData); // FishTankManager must be setup first before FishManager
        fishManager.Setup(this, fishDataList);
        popupManager.Setup(this);
    }

    // Load Fish Data from DataManager.
    private List<FishData> LoadFishDataList()
    {
        if (GameManager.instance.dataManager.fishDataList != null)
        {
            return GameManager.instance.dataManager.fishDataList;
        }
        return new List<FishData>(); // TO DO : need to handle the error situation.
    }

    // Load User Game data (ex. fish tank level)
    private GameData LoadGameData()
    {
        if (GameManager.instance.dataManager.gameData != null)
        {
            return GameManager.instance.dataManager.gameData;
        }
        return null;
    }
}
