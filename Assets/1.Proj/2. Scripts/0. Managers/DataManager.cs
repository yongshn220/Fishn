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
    public void SaveSeaObjectData(List<SeaObjectMono> seaObjectMonoList)
    {
        this.seaObjectDataList = seaObjectMonoList.ConvertToData();
        DatabaseHelper.SaveSeaObjectData(seaObjectDataList).Forget();
    }
#endregion

#region Add
    public void AddSeaObject(int type_id)
    {
        SeaObjectData seaObjectData = new SeaObjectData(-1, type_id, Vector3.zero, true);
        DatabaseHelper.AddSeaObjectData(seaObjectData).Forget();
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


