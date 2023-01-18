
using System;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


/*
    1. Convert the result data into json format -> send to Database.
    2. Convert the unity data into json format -> send to Database.
*/
public static class DatabaseHelper
{
    static Database database = new Database();
    static string uid = SystemInfo.deviceUniqueIdentifier;

#region LOAD
    public static async UniTask<UserData> AsyncLoadUserData()
    {
        string res = await database.AsyncLoadUserData(uid);

        JObject json = JObject.Parse(res);
        return GetUserDataFromJson(json);
    }
#endregion

#region SAVE
    public static async UniTaskVoid SaveSeaObjectData(List<SeaObjectData> seaObjectDataList)
    {
        JArray jArray = new JArray();
        
        foreach(SeaObjectData data in seaObjectDataList)
        {
            jArray.Add(ConvertSeaObjectToJson(data));
        }

        Debug.Log(JsonConvert.SerializeObject(jArray));

        string res = await database.AsyncSaveSeaObjectData(uid, jArray);
        Debug.Log(res);
    }
#endregion

#region Convert Class to JSON
    private static JObject ConvertSeaObjectToJson(SeaObjectData seaObjectData)
    {
        JObject jObject = new JObject();
        jObject[DBstr.ID] = seaObjectData.id;
        jObject[DBstr.TYPE_ID] = seaObjectData.type_id;
        jObject[DBstr.POSX] = seaObjectData.position.x;
        jObject[DBstr.POSY] = seaObjectData.position.y;
        jObject[DBstr.POSZ] = seaObjectData.position.z;
        jObject[DBstr.INSTANTIATED] = seaObjectData.instantiated;
        return jObject;
    }
#endregion

#region Convert Json to Class
    private static UserData GetUserDataFromJson(JObject json)
    {
        UserData userData = new UserData();
        userData.gameData = GetGameDataFromJson(json[DBstr.GAMEDATA]);
        userData.entityDataList = GetEntityDataListFromJson(json[DBstr.ENTITYDATALIST]);
        userData.seaObjectDataList =  GetSeaObjectDataListFromJson(json[DBstr.SEAOBJECTDATALIST]);

        return userData;
    }

    private static GameData GetGameDataFromJson(JToken gameDataJson)
    {
        int id = (int) gameDataJson[DBstr.ID];
        int tank_id = (int) gameDataJson[DBstr.TANK_ID];
        int coral = (int) gameDataJson[DBstr.CORAL];
        return new GameData(id, tank_id, coral);
    }

    private static List<SeaObjectData> GetSeaObjectDataListFromJson(JToken seaPlantDataJson)
    {
        List<SeaObjectData> seaObjectDataList = new List<SeaObjectData>();
        foreach (var plant in seaPlantDataJson)
        {
            int id = (int) plant[DBstr.ID];
            int type_id = (int) plant[DBstr.TYPE_ID];
            float posx = (float) plant[DBstr.POSX];
            float posy = (float) plant[DBstr.POSY];
            float posz = (float) plant[DBstr.POSZ];
            bool instantiated = (bool) plant[DBstr.INSTANTIATED];
            Vector3 position = new Vector3(posx, posy, posz);

            GameObject tempObject = new GameObject();
            SeaObjectData seaObjectData = tempObject.AddComponent<SeaObjectData>();

            seaObjectData.Setup(id, type_id, position, instantiated);
            seaObjectDataList.Add(seaObjectData);
        }
        return seaObjectDataList;
    }

    private static List<EntityData> GetEntityDataListFromJson(JToken fishDataJson)
    {
        List<EntityData> entityDataList = new List<EntityData>();

        foreach(var fish in fishDataJson)
        {
            int id = (int) fish[DBstr.ID];
            int type_id = (int) fish[DBstr.TYPE_ID];
            DateTime born_datetime = DateTime.Parse((string) fish[DBstr.BORN_DATETIME]); // TO DO : handling parse fail.
            DateTime feed_datetime = DateTime.Parse((string) fish[DBstr.FEED_DATETIME]); // TO DO : handling parse fail.
            
            GameObject tempObject = new GameObject();
            EntityData entityData = tempObject.AddComponent<EntityData>();

            entityData.Setup(id, type_id, born_datetime, feed_datetime);
            entityDataList.Add(entityData);
        }
        return entityDataList;
    }
#endregion
    private static void PrintUserData()
    {
        // Debug.Log(gameData.ToString());
        
        // foreach(var f in fishDataList)
        // {
        //     Debug.Log(f.ToString());
        // }
        // foreach(var f in seaPlantDataList)
        // {
        //     Debug.Log(f.ToString());
        // }
    }
}
