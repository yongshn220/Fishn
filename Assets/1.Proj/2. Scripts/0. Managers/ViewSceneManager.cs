using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSceneManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public FishTankManager fishTankManager;
    public FishManager fishManager;
    public PopupManager popupManager;

    List<FishData> fishDataList;
    GameData gameData;

    void Awake()
    {
        cameraManager = GetComponentInChildren<CameraManager>();
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

#region Setup
    private void Setup()
    {
        GameManager.instance.SetViewSceneManager(this);
        fishTankManager.Setup(this, gameData); // Setup Order 1 : FishTankManager must be setup first before FishManager
        cameraManager.Setup(this);             // Setup Order 2
        fishManager.Setup(this, fishDataList); // Setup Order 3
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
#endregion
}
