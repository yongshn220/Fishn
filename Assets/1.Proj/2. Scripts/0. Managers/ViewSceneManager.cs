using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSceneManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public FishTankManager fishTankManager;
    public FishManager fishManager;
    public PopupManager popupManager;

    List<EntityData> entityDataList;
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
        entityDataList = LoadEntityDataList();
        gameData = LoadGameData();

        Setup();
    }

#region Setup
    private void Setup()
    {
        GameManager.instance.SetViewSceneManager(this);
        fishTankManager.Setup(this, gameData); // Setup Order 1 : FishTankManager must be setup first before FishManager
        cameraManager.Setup(this);             // Setup Order 2
        fishManager.Setup(this, entityDataList); // Setup Order 3
        popupManager.Setup(this);
    }

    // Load Fish Data from DataManager.
    private List<EntityData> LoadEntityDataList()
    {
        if (GameManager.instance.dataManager.entityDataList != null)
        {
            return GameManager.instance.dataManager.entityDataList;
        }
        return new List<EntityData>(); // TO DO : need to handle the error situation.
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
