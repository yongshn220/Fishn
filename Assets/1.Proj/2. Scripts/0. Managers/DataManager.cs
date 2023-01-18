using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;

/*
    1. Receive Json data from DatabaseHelper -> apply to Unity.
    2. Receive Unity data from All Unity classes -> send to DatabaseHelper.
*/
public class DataManager : MonoBehaviour
{
    public bool isDataReady = false;

    JObject jsonData;

    public GameData gameData = null;
    public List<SeaObjectData> seaPlantDataList = new List<SeaObjectData>();
    public List<FishData> fishDataList = new List<FishData>();

#region Load
    public void LoadUserData()
    {
        Action<JObject> callback = (json) =>
        {
            SetDataFromJson(json); // TO DO : set data fail handling.
            isDataReady = true;
        };
        DatabaseHelper.LoadUserData(callback).Forget();
    }
#endregion

#region Save
    public void SaveSeaObjectData(List<SeaObjectData> seaObjectDataList)
    {
        DatabaseHelper.SaveSeaObjectData(seaObjectDataList);
    }
#endregion

    private void SetDataFromJson(JObject json)
    {
        jsonData = json;
        SetGameDataFromJson(jsonData["gamedata"]);
        SetSeaPlantDataFromJson(jsonData["seaplantList"]);
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

            SeaObjectData newData = new SeaObjectData();
            newData.Setup(id, type_id, position);
            seaPlantDataList.Add(newData);
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
