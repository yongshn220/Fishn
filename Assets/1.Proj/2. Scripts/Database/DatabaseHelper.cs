
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
    static string UID = SystemInfo.deviceUniqueIdentifier;

#region Load
    public static async UniTask<UserData> AsyncLoadUserData()
    {
        string res = await database.AsyncLoadUserData(UID);

        JObject json = JObject.Parse(res);
        return GetUserDataFromJson(json);
    }
#endregion

#region Save
    public static async UniTaskVoid SaveCoral(int coral)
    {
        JObject jObject = new JObject();
        jObject[DBstr.CORAL] = coral;
        string res = await database.AsyncSaveCoral(UID, jObject);
    }

    public static async UniTaskVoid SaveSeaObjectData(List<SeaObjectData> seaObjectDataList)
    {
        JArray jArray = new JArray();
        
        foreach(SeaObjectData data in seaObjectDataList)
        {
            jArray.Add(ConvertSeaObjectToJson(data));
        }
        string res = await database.AsyncSaveSeaObjectData(UID, jArray);
    }

    public static async UniTaskVoid SaveCoralPlantData(List<CoralPlantData> coralPlantDataList)
    {
        JArray jArray = new JArray();
        
        foreach(CoralPlantData data in coralPlantDataList)
        {
            jArray.Add(ConvertCoralPlantToJson(data));
        }
        string res = await database.AsyncSaveCoralPlantData(UID, jArray);
    }

    public static async UniTaskVoid SaveEntityData(EntityData entityData)
    {
        JObject jObject = new JObject();
        jObject = ConvertEntityToJson(entityData);
        string res = await database.AsyncSaveEntityData(UID, jObject);
    }

    public static async UniTask<string> SaveFishTankID(int tank_id)
    {
        JObject jObject = new JObject();
        jObject[DBstr.TANK_ID] = tank_id;
        string res = await database.AsyncSaveFishTankID(UID, jObject);
        return res;
    }
#endregion

#region Add
    public static async UniTask<int> AddSeaObjectData(SeaObjectData seaObjectData)
    {
        JObject jObject = new JObject();
        jObject = ConvertSeaObjectToJson(seaObjectData);
        string id = await database.AsyncAddSeaObjectData(UID, jObject);
        return int.Parse(id);
    }

    public static async UniTask<int> AddEntityData(EntityData entityData)
    {
        JObject jObject = new JObject();
        jObject = ConvertEntityToJson(entityData);
        string id = await database.AsyncAddEntityData(UID, jObject);
        Debug.Log(id);
        return int.Parse(id);
    }

    public static async UniTask<int> AddCoralPlant(CoralPlantData coralPlantData)
    {
        JObject jObject = new JObject();
        jObject = ConvertCoralPlantToJson(coralPlantData);
        string id = await database.AsyncAddCoralPlantData(UID, jObject);
        return int.Parse(id);

    }
#endregion

#region Convert Class to JSON
    private static JObject ConvertEntityToJson(EntityData entityData)
    {
        JObject jObject = new JObject();
        jObject[DBstr.ID] = entityData.id;
        jObject[DBstr.TYPE_ID] = entityData.type_id;
        jObject[DBstr.BORN_DATETIME] = entityData.born_datetime.ToFormatString();
        jObject[DBstr.FEED_DATETIME] = entityData.feed_datetime.ToFormatString();
        jObject[DBstr.FEED] = entityData.feed;
        return jObject;
    }

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

    private static JObject ConvertCoralPlantToJson(CoralPlantData coralPlantData)
    {
        JObject jObject = new JObject();
        jObject[DBstr.ID] = coralPlantData.id;
        jObject[DBstr.TYPE_ID] = coralPlantData.type_id;
        jObject[DBstr.POSX] = coralPlantData.position.x;
        jObject[DBstr.POSY] = coralPlantData.position.y;
        jObject[DBstr.POSZ] = coralPlantData.position.z;
        jObject[DBstr.INSTANTIATED] = coralPlantData.instantiated;
        return jObject;
    }
#endregion

#region Convert Json to Class
    private static UserData GetUserDataFromJson(JObject json)
    {
        UserData userData = new UserData();
        userData.gameData = GetGameDataFromJson(json[DBstr.GAMEDATA]);
        userData.entityDataList = GetEntityDataListFromJson(json[DBstr.ENTITYDATA_LIST]);
        userData.seaObjectDataList = GetSeaObjectDataListFromJson(json[DBstr.SEAOBJECTDATA_LIST]);
        userData.coralPlantDataList = GetCoralPlantDataListFronJson(json[DBstr.CORALPLANTDATA_LIST]);

        return userData;
    }

    private static GameData GetGameDataFromJson(JToken gameDataJson)
    {
        int id = (int) gameDataJson[DBstr.ID];
        int tank_id = (int) gameDataJson[DBstr.TANK_ID];
        int coral = (int) gameDataJson[DBstr.CORAL];
        return new GameData(id, tank_id, coral);
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
            int feed = (int) fish[DBstr.FEED];
            EntityData entityData = new EntityData(id, type_id, born_datetime, feed_datetime, feed);
            entityDataList.Add(entityData);
        }
        return entityDataList;
    }

    private static List<SeaObjectData> GetSeaObjectDataListFromJson(JToken seaObjectDataJson)
    {
        List<SeaObjectData> seaObjectDataList = new List<SeaObjectData>();
        foreach (var plant in seaObjectDataJson)
        {
            int id = (int) plant[DBstr.ID];
            int type_id = (int) plant[DBstr.TYPE_ID];
            float posx = (float) plant[DBstr.POSX];
            float posy = (float) plant[DBstr.POSY];
            float posz = (float) plant[DBstr.POSZ];
            bool instantiated = (bool) plant[DBstr.INSTANTIATED];
            Vector3 position = new Vector3(posx, posy, posz);

            SeaObjectData seaObjectData = new SeaObjectData(id, type_id, position, instantiated);
            seaObjectDataList.Add(seaObjectData);
        }
        return seaObjectDataList;
    }

    private static List<CoralPlantData> GetCoralPlantDataListFronJson(JToken coralPlantDataJson)
    {
        List<CoralPlantData> coralPlantDataList = new List<CoralPlantData>();
        foreach (var plant in coralPlantDataJson)
        {
            int id = (int) plant[DBstr.ID];
            int type_id = (int) plant[DBstr.TYPE_ID];
            float posx = (float) plant[DBstr.POSX];
            float posy = (float) plant[DBstr.POSY];
            float posz = (float) plant[DBstr.POSZ];
            bool instantiated = (bool) plant[DBstr.INSTANTIATED];
            Vector3 position = new Vector3(posx, posy, posz);

            CoralPlantData coralPlantData = new CoralPlantData(id, type_id, position, instantiated);
            coralPlantDataList.Add(coralPlantData);
        }
        return coralPlantDataList;
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
