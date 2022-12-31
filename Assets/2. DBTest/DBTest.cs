using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBTest : MonoBehaviour
{
    string url = "https://bf34qg8u25.execute-api.us-east-2.amazonaws.com/default/lambdaTest";

    IEnumerator Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("command", "register");
        form.AddField("uid", "123456");
        form.AddField("password", "dydwjd2200");

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        print(www.downloadHandler.text);
    }
}