using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class Database
{
    const string URL = "https://w6yc5awthi.execute-api.us-east-2.amazonaws.com/default/Fishn-maindb";
    
    public async UniTask<string> AsyncLoadUserData(string uid)
    {
        WWWForm form = new WWWForm();

        form.AddField("command", "login");
        form.AddField("uid", uid);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        await www.SendWebRequest();

        return www.downloadHandler.text;
    }

    public async UniTaskVoid AsyncSaveSeaObjectData(string uid, JArray jarray)
    {
    }
}

