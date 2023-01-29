using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTankManager : MonoBehaviour
{
    private ViewSceneManager sceneManager;
    private Transform myTransform;
    private FishTankController fishTankController;

    void Awake()
    {
        myTransform = transform;
    }

    public void Setup(ViewSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        int tank_id = GameManager.instance.dataManager.GetUserTankId();

        if (tank_id < 0)
        {
            Debug.LogError("No Tank_id is provided.");
            return;
        }

        LoadFishTank(tank_id);
    }

    private void LoadFishTank(int tank_id)
    {
        GameObject prefab = GameManager.instance.scriptableObjectManager.TryGetFishTankPrefabById(tank_id);
        if (prefab)
        {
            GameObject fishTankObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, myTransform); // Tip 1 : Awake() of prefab is called when it instantiated.
            fishTankController = fishTankObject.GetComponent<FishTankController>();                          // Tip 2 : The script in instantiated Object and in Prefab are difference scripts.
            fishTankController.Setup();
        }
    }

    public CameraContainer GetCameraContainer()
    {
        return fishTankController.GetCameraContainer();
    }

    public List<SeaObjectData> GetDisabledSeaObjectDataList()
    {
        return fishTankController.disabledSeaObjectDataListDeepCopy;
    }

    public List<CoralPlantData> GetDisabledCoralPlantDataList()
    {
        return fishTankController.disabledCoralPlantDataListDeepCopy;
    }

    public void UpdateAllSeaObjectMonoPosition()
    {
        fishTankController.UpdateAllSeaObjectMonoPosition();
    }

    // Take seaObject out from the bag to tank.
    public void RemoveSeaObjectFromTank(SeaObjectMono seaObjectMono)
    {
        fishTankController.RemoveSeaObjectFromTank(seaObjectMono);
    }

    public void RemoveCoralPlantFromTank(CoralPlantMono coralPlantMono)
    {
        fishTankController.RemoveCoralPlantFromTank(coralPlantMono);
    }

    // Put seaObject in the bag from tank.
    public void LoadSeaObjectFromBag(int type_id)
    {
        fishTankController.LoadSeaObjectFromBag(type_id);
    }

    public void LoadCoralPlantFromBag(int type_id)
    {
        fishTankController.LoadCoralPlantFromBag(type_id);
    }

    // Called when a new fish added.
    public void InstantiateSeaObject(SeaObjectData seaObjectData)
    {
        fishTankController.InstantiateSeaObject(seaObjectData);
    }

    public void InstantiateCoralPlant(CoralPlantData coralPlantData)
    {
        fishTankController.InstantiateCoralPlant(coralPlantData);
    }

    public void SetCoralLightingState(bool state)
    {
        fishTankController.SetCoralLightingState(state);
    }
}
