
using System;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
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
    public static void SaveSeaObjectData(List<SeaObjectData> seaObjectDataList)
    {
        // JArray jArray = new JArray();
        
        // foreach(SeaObjectData data in seaObjectDataList)
        // {
        //     jArray.Add(ConvertSeaObjectToJson(data));
        // }

        // Database.AsyncSaveSeaObjectData(jArray).Forget();
    }
#endregion
    
    private static JObject ConvertSeaObjectToJson(SeaObjectData seaObjectData)
    {
        JObject jObject = new JObject();
        jObject["id"] = seaObjectData.id;
        jObject["type_id"] = seaObjectData.type_id;
        jObject["posx"] = seaObjectData.position.x;
        jObject["posy"] = seaObjectData.position.y;
        jObject["posz"] = seaObjectData.position.z;
        return jObject;
    }
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
        int id = (int) gameDataJson["id"];
        int tank_id = (int) gameDataJson["tank_id"];
        int coral = (int) gameDataJson["coral"];
        return new GameData(id, tank_id, coral);
    }

    private static List<SeaObjectData> GetSeaObjectDataListFromJson(JToken seaPlantDataJson)
    {
        List<SeaObjectData> seaObjectDataList = new List<SeaObjectData>();
        foreach (var plant in seaPlantDataJson)
        {
            int id = (int) plant["id"];
            int type_id = (int) plant["type_id"];
            float posx = (float) plant["posx"];
            float posy = (float) plant["posy"];
            float posz = (float) plant["posz"];
            Vector3 position = new Vector3(posx, posy, posz);

            SeaObjectData newData = new SeaObjectData();
            newData.Setup(id, type_id, position);
            seaObjectDataList.Add(newData);
        }
        return seaObjectDataList;
    }

    private static List<EntityData> GetEntityDataListFromJson(JToken fishDataJson)
    {
        List<EntityData> entityDataList = new List<EntityData>();

        foreach(var fish in fishDataJson)
        {
            int id = (int) fish["id"];
            int type_id = (int) fish["type_id"];
            DateTime born_datetime = DateTime.Parse((string) fish["born_datetime"]); // TO DO : handling parse fail.
            DateTime feed_datetime = DateTime.Parse((string) fish["feed_datetime"]); // TO DO : handling parse fail.
            entityDataList.Add(new EntityData(id, type_id, born_datetime, feed_datetime));
        }
        return entityDataList;
    }

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
