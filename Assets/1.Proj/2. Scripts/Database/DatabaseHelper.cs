
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

    public static async UniTaskVoid LoadUserData(Action<JObject> callback)
    {
        string uid = SystemInfo.deviceUniqueIdentifier;
        
        string res = await database.AsyncLoadUserData(uid);

        JObject json = JObject.Parse(res);
        callback(json);
    }

    public static void SaveSeaObjectData(List<SeaObjectData> seaObjectDataList)
    {
        JArray jArray = new JArray();
        
        foreach(SeaObjectData data in seaObjectDataList)
        {
            jArray.Add(ConvertSeaObjectToJson(data));
        }
    }

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
}
