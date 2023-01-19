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
    
     public async UniTask<string> AsyncLoadUserData(string uid)
    {
        JObject json = new JObject();
        json["uid"] = uid;
        return await AsyncPostWebRequest(json, "login");
    }

    public async UniTask<string> AsyncSaveSeaObjectData(string uid, JArray jArray)
    {
        JObject json = new JObject();
        json["data"] = jArray;
        json["uid"] = uid;
        return await AsyncPostWebRequest(json, "save/seaobjects");
    }

    private async UniTask<string> AsyncPostWebRequest(JObject json, string contentType)
    {
        UnityWebRequest request = new UnityWebRequest(URL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", contentType);

        await request.SendWebRequest();
        return request.downloadHandler.text;
    }

    // public async UniTask<string> AsyncLoadUserData(string uid)
    // {
    //     WWWForm form = new WWWForm();
    //     form.AddField(DBstr.COMMAND, DBstr.LOGIN);
    //     form.AddField(DBstr.UID, uid);

    //     return await AsyncSendPostRequest(form);
    // }

    // private async UniTask<string> AsyncSendPostRequest(WWWForm form)
    // {
    //     UnityWebRequest www = UnityWebRequest.Post(URL, form);
    //     await www.SendWebRequest();
    //     return www.downloadHandler.text;
    // }
}

