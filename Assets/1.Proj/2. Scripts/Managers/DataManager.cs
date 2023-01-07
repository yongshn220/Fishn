using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;

public class DataManager : MonoBehaviour
{
    public bool isDataReady = false;

    JObject jsonData;


    public GameData gameData = null;

    private List<FishData> _fishDataList = new List<FishData>();
    public List<FishData> fishDataList { get {return _fishDataList; } set {_fishDataList = value; }}

    public void LoadUserData()
    {
        Action<JObject> callback = (json) =>
        {
            SetDataFromJson(json); // TO DO : set data fail handling.
            isDataReady = true;
        };
        DatabaseHelper.LoadUserData(callback).Forget();
    }

    private void SetDataFromJson(JObject json)
    {
        jsonData = json;
        SetGameDataFromJson(jsonData["gamedata"]);
        SetFishDataFromJson(jsonData["fishList"]);

        print(gameData.ToString());
        
        foreach(var f in fishDataList)
        {
            print(f.ToString());
        }
    }

    private void SetGameDataFromJson(JToken gameDataJson)
    {
        int id = (int) gameDataJson["id"];
        int tank_id = (int) gameDataJson["tank_id"];
        gameData = new GameData(id, tank_id);
    }

    private void SetFishDataFromJson(JToken fishDataJson)
    {
        fishDataList.Clear();

        foreach(var fish in fishDataJson)
        {
            int id = (int) fish["id"];
            int type_id = (int) fish["type_id"];
            DateTime born_datetime = DateTime.Parse((string) fish["born_datetime"]); // TO DO : handling parse fail.
            DateTime feed_datetime = DateTime.Parse((string) fish["feed_datetime"]); // TO DO : handling parse fail.
            fishDataList.Add(new FishData(id, type_id, born_datetime, feed_datetime));
        }
    }
}


public class GameData
{
    public int id;
    public int tank_id;

    public GameData(int id, int tank_id)
    {
        this.id = id;
        this.tank_id = tank_id;
    }


    public override string ToString()
    {
        return $"id = {id}, tank_id = {tank_id}";
    }
}

public class FishData
{
    public int id;
    public int type_id;
    public DateTime born_datetime;
    public DateTime feed_datetime;

    public FishData(int id, int type_id, DateTime born_datetime, DateTime feed_datetime)
    {
        this.id = id;
        this.type_id = type_id;
        this.born_datetime = born_datetime;
        this.feed_datetime = feed_datetime;
    }

    public override string ToString()
    {
        return $"id = {id}, type_id = {type_id}, born_datetime = {born_datetime}, feed_datetime = {feed_datetime}";
    }
}