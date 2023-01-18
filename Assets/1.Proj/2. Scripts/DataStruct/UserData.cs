using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public GameData gameData;
    public List<EntityData> entityDataList;
    public List<SeaObjectData> seaObjectDataList;

    public bool IsUserDataValid()
    {
        return gameData != null && entityDataList.Count > 0;
    }
}
