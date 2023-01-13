using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptableObjectManager : MonoBehaviour
{
    public EntityScriptableObject EntityList;

    public SeaPlantScriptableObject SeaPlantList;

    public RockScriptableObject RockList;

    public FishTankScriptableObject FishTankList;

    public GameObject TryGetEntityPrefabById(int id)
    {
        foreach (var entity in EntityList.Entity)
        {
            if (entity.id == id)
            {
                return entity.prefab;
            }
        }
        return null;
    }

    public GameObject TryGetFishTankPrefabById(int id)
    {
        foreach (var tank in FishTankList.fishTanks)
        {
            if (tank.id == id)
            {
                return tank.prefab;
            }
        }
        return null;
    }
}

