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
    public List<SeaPlantData> seaPlantDataList = new List<SeaPlantData>();
    public List<RockData> rockDataList = new List<RockData>();
    public List<FishData> fishDataList = new List<FishData>();

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
        SetSeaPlantDataFromJson(jsonData["seaplantList"]);
        SetRockDataFromJson(jsonData["rockList"]);
        SetFishDataFromJson(jsonData["fishList"]);

        PrintUserData();
    }

    private void PrintUserData()
    {
        print(gameData.ToString());
        
        foreach(var f in fishDataList)
        {
            print(f.ToString());
        }
        foreach(var f in seaPlantDataList)
        {
            print(f.ToString());
        }
        foreach(var f in rockDataList)
        {
            print(f.ToString());
        }
    }

    private void SetGameDataFromJson(JToken gameDataJson)
    {
        int id = (int) gameDataJson["id"];
        int tank_id = (int) gameDataJson["tank_id"];
        int coral = (int) gameDataJson["coral"];
        gameData = new GameData(id, tank_id, coral);
    }

    private void SetSeaPlantDataFromJson(JToken seaPlantDataJson)
    {
        seaPlantDataList.Clear();

        foreach (var plant in seaPlantDataJson)
        {
            int id = (int) plant["id"];
            int type_id = (int) plant["type_id"];
            float posx = (float) plant["posx"];
            float posy = (float) plant["posy"];
            float posz = (float) plant["posz"];
            Vector3 position = new Vector3(posx, posy, posz);

            seaPlantDataList.Add(new SeaPlantData(id, type_id, position));
        }


    }

    private void SetRockDataFromJson(JToken rockDataJson)
    {
        rockDataList.Clear();

        foreach (var rock in rockDataJson)
        {
            int id = (int) rock["id"];
            int type_id = (int) rock["type_id"];
            float posx = (float) rock["posx"];
            float posy = (float) rock["posy"];
            float posz = (float) rock["posz"];
            Vector3 position = new Vector3(posx, posy, posz);
            
            rockDataList.Add(new RockData(id, type_id, position));
        }
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
        Debug.Log(fishDataList.Count);
    }
}


public class GameData
{
    public int id;
    public int tank_id;
    public int coral;

    public GameData(int id, int tank_id, int coral)
    {
        this.id = id;
        this.tank_id = tank_id;
        this.coral = coral;
    }


    public override string ToString()
    {
        return $"id = {id}, tank_id = {tank_id}, coral = {coral}";
    }
}

public class SeaPlantData
{
    public int id;
    public int type_id;
    public Vector3 position;

    public SeaPlantData(int id, int type_id, Vector3 position)
    {
        this.id = id;
        this.type_id = type_id;
        this.position = position;
    }

    public override string ToString()
    {
        return $"id = {id}, position = {position}";
    }
}

public class RockData
{
    public int id;
    public int type_id;
    public Vector3 position;

    public RockData(int id, int type_id, Vector3 position)
    {
        this.id = id;
        this.type_id = type_id;
        this.position = position;
    }

    public override string ToString()
    {
        return $"id = {id}, position = {position}";
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