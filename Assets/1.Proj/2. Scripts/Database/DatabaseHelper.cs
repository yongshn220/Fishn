
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
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
}