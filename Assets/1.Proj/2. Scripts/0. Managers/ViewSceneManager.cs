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

        Setup();
    }

#region Setup
    private void Setup()
    {
        GameManager.instance.SetViewSceneManager(this);
        fishTankManager.Setup(this);             // Setup Order 1 
        cameraManager.Setup(this);               // Setup Order 2 : require fishTankMgr setup 
        fishManager.Setup(this);                 // Setup Order 3 : require fishTankMgr setup
        popupManager.Setup(this);                // Setup Order 4 : require fishTankMgr setup
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
#endregion
}
