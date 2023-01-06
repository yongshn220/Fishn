using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class Database
{
    const string URL = "https://w6yc5awthi.execute-api.us-east-2.amazonaws.com/default/Fishn-maindb";
    
    public async UniTask<string> AsyncLoadGameData(string uid)
    {
        WWWForm form = new WWWForm();

        form.AddField("command", "login");
        form.AddField("uid", uid);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        await www.SendWebRequest();

        return www.downloadHandler.text;
    }
}
