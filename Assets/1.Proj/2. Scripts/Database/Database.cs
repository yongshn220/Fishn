using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

public class Database
{
    const string URL = "https://w6yc5awthi.execute-api.us-east-2.amazonaws.com/default/Fishn-maindb";

#region Load
     public async UniTask<string> AsyncLoadUserData(string uid)
    {
        JObject json = new JObject();
        json["uid"] = uid;
        return await AsyncPostWebRequest(json, "login");
    }
#endregion

#region Save
    public async UniTask<string> AsyncSaveSeaObjectData(string uid, JArray jArray)
    {
        JObject json = new JObject();
        json["uid"] = uid;
        json["data"] = jArray;
        return await AsyncPostWebRequest(json, "save/seaobjects");
    }
#endregion

#region Add
    public async UniTask<string> AsyncAddSeaObjectData(string uid, JObject jObject)
    {
        JObject json = new JObject();
        json["uid"] = uid;
        json["data"] = jObject;
        return await AsyncPostWebRequest(json, "add/seaobject");
    }

    public async UniTask<string> AsyncAddEntityData(string uid, JObject jObject)
    {
        JObject json = new JObject();
        json["uid"] = uid;
        json["data"] = jObject;
        return await AsyncPostWebRequest(json, "add/entity");
    }

    public async UniTask<string> AsyncAddCoralPlantData(string uid, JObject jObject)
    {
        JObject json = new JObject();
        json["uid"] = uid;
        json["data"] = jObject;
        return await AsyncPostWebRequest(json, "add/coralplant");
    }
#endregion

    private async UniTask<string> AsyncPostWebRequest(JObject json, string contentType)
    {
        UnityWebRequest request = new UnityWebRequest(URL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", contentType);

        await request.SendWebRequest();
        string result = request.downloadHandler.text;
        request.Dispose();
        return result;
    }
}

