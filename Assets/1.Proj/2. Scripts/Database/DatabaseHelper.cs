
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

public static class DatabaseHelper
{
    static Database database = new Database();

    public static async UniTask<JObject> LoadGameData()
    {
        string uid = SystemInfo.deviceUniqueIdentifier;
        
        string res = await database.AsyncLoadGameData(uid);
        
        JObject json = StringToJson(res);
        return json;
    }

    private static JObject StringToJson(string str)
    {
        JObject json = JObject.Parse(str);
        return json;
    }
}