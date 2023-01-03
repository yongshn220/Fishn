
using UnityEngine;
using Cysharp.Threading.Tasks;


public static class DatabaseHelper
{
    static Database database = new Database();

    public static async UniTask<string> LoadGameData()
    {
        string uid = SystemInfo.deviceUniqueIdentifier;
        
        string res = await database.AsyncLoadGameData(uid);

        return res;
    }
}
