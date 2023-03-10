using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;

/*
    1. Receive Json data from DatabaseHelper -> apply to Unity.
    2. Receive Unity data from All Unity classes -> send to DatabaseHelper.
*/
public class DataManager : MonoBehaviour
{
    public bool isDataReady = false;

    // Raw Data
    private UserData userData;
    private GameData gameData = null;
    public List<EntityData> entityDataList = new List<EntityData>();
    public List<SeaObjectData> seaObjectDataList = new List<SeaObjectData>();
    public List<CoralPlantData> coralPlantDataList = new List<CoralPlantData>();

#region Load

    public void LoadUserData() => AsyncLoadUserData().Forget();

    public async UniTask<bool> AsyncLoadUserData()
    {
        userData = await DatabaseHelper.AsyncLoadUserData();
        gameData = userData.gameData;
        entityDataList = userData.entityDataList;
        seaObjectDataList = userData.seaObjectDataList;
        coralPlantDataList = userData.coralPlantDataList;

        Wallet.SetCoral(gameData.coral); 
        isDataReady = true;
        DelegateManager.InvokeOnUserDataLoad();
        return true;
    }

#endregion

#region Save
    public void SaveCoral(int coral)
    {
        DatabaseHelper.SaveCoral(coral).Forget();
    }

    public void SaveSeaObjectData(List<SeaObjectData> seaObjectDataList)
    {
        this.seaObjectDataList = seaObjectDataList; // Sync Data
        DatabaseHelper.SaveSeaObjectData(seaObjectDataList).Forget();
    }

    public void SaveCoralPlantData(List<CoralPlantData> coralPlantDataList)
    {
        this.coralPlantDataList = coralPlantDataList; // Sync Data
        DatabaseHelper.SaveCoralPlantData(coralPlantDataList).Forget();
    }

    public void SaveEntityData(EntityData entityData)
    {
        DatabaseHelper.SaveEntityData(entityData).Forget();
    }

    public void SaveFishTankID(int tank_id)
    {
        DatabaseHelper.SaveFishTankID(tank_id).Forget();
    }

    public async UniTask<bool> AsyncSaveFishTankID(int tank_id)
    {
        await DatabaseHelper.SaveFishTankID(tank_id);
        return true;
    }
#endregion

#region Add
    public async UniTask<int> AddSeaObject(SeaObjectData newData)
    {
        int id = await DatabaseHelper.AddSeaObjectData(newData);
        return id;
    }

    public async UniTask<int> AddEntity(EntityData newData)
    {
        int id = await DatabaseHelper.AddEntityData(newData);
        return id;
    }

    public async UniTask<int> AddCoralPlant(CoralPlantData newData)
    {
        int id = await DatabaseHelper.AddCoralPlant(newData);
        return id;
    }

#endregion

#region Remove
    public async UniTask<string> RemoveEntity(int id)
    {
        return await DatabaseHelper.RemoveEntityData(id);
    }
#endregion

    public int GetUserTankId()
    {
        return (gameData != null)? gameData.tank_id : -1;
    }
}


