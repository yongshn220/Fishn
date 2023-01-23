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

    private UserData userData;
    private GameData gameData = null;
    public List<SeaObjectData> seaObjectDataList = new List<SeaObjectData>();
    public List<EntityData> entityDataList = new List<EntityData>();


#region Load

    public void LoadUserData() => AsyncLoadUserData().Forget();

    public async UniTaskVoid AsyncLoadUserData()
    {
        userData = await DatabaseHelper.AsyncLoadUserData();
        gameData = userData.gameData;
        seaObjectDataList = userData.seaObjectDataList;
        entityDataList = userData.entityDataList;

        Fishn.Wallet.SetCoral(gameData.coral); 
        isDataReady = true;
    }

#endregion

#region Save
    public void SaveSeaObjectData(List<SeaObjectData> seaObjectDataList)
    {
        this.seaObjectDataList = seaObjectDataList;
        DatabaseHelper.SaveSeaObjectData(seaObjectDataList).Forget();
    }
#endregion

#region Add
    public async UniTask AddSeaObject(SeaObjectData newData)
    {
        await DatabaseHelper.AddSeaObjectData(newData);
    }

    public void AddEntity(int type_id)
    {

    }
#endregion

    public int GetUserTankId()
    {
        return (gameData != null)? gameData.tank_id : -1;
    }
}


