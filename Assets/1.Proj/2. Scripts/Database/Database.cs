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
    public async UniTask<string> AsyncSaveCoral(string uid, JObject jObject)
    {
        JObject json = CreateRequestJObject(uid, jObject);
        return await AsyncPostWebRequest(json, "save/coral");
    }

    public async UniTask<string> AsyncSaveSeaObjectData(string uid, JArray jArray)
    {
        JObject json = CreateRequestJArray(uid, jArray);
        return await AsyncPostWebRequest(json, "save/seaobjects");
    }

    public async UniTask<string> AsyncSaveCoralPlantData(string uid, JArray jArray)
    {
        JObject json = CreateRequestJArray(uid, jArray);
        return await AsyncPostWebRequest(json, "save/coralplants");
    }

    public async UniTask<string> AsyncSaveEntityData(string uid, JObject jObject)
    {
        JObject json = CreateRequestJObject(uid, jObject);
        return await AsyncPostWebRequest(json, "save/entity");
    }

    public async UniTask<string> AsyncSaveFishTankID(string uid, JObject jObject)
    {
        JObject json = CreateRequestJObject(uid, jObject);
        return await AsyncPostWebRequest(json, "save/tank_id");
    }
#endregion

#region Add
    public async UniTask<string> AsyncAddSeaObjectData(string uid, JObject jObject)
    {
        JObject json = CreateRequestJObject(uid, jObject);
        return await AsyncPostWebRequest(json, "add/seaobject");
    }

    public async UniTask<string> AsyncAddEntityData(string uid, JObject jObject)
    {
        JObject json = CreateRequestJObject(uid, jObject);
        return await AsyncPostWebRequest(json, "add/entity");
    }

    public async UniTask<string> AsyncAddCoralPlantData(string uid, JObject jObject)
    {
        JObject json = CreateRequestJObject(uid, jObject);
        return await AsyncPostWebRequest(json, "add/coralplant");
    }
#endregion

#region Remove
    public async UniTask<string> AsyncRemoveEntityData(string uid, JObject jObject)
    {
        JObject json = CreateRequestJObject(uid, jObject);
        return await AsyncPostWebRequest(json, "remove/entity");
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

    private JObject CreateRequestJObject(string uid, JObject jObject)
    {
        JObject json = new JObject();
        json["uid"] = uid;
        json["data"] = jObject;
        return json;
    }

    private JObject CreateRequestJArray(string uid, JArray jArray)
    {
        JObject json = new JObject();
        json["uid"] = uid;
        json["data"] = jArray;
        return json;
    }
}

