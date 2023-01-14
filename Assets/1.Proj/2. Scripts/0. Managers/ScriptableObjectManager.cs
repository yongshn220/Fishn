using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ScriptableObjectManager : MonoBehaviour
{
    public EntityScriptableObject EntityList;

    public SeaPlantScriptableObject SeaPlantList;

    public RockScriptableObject RockList;

    public FishTankScriptableObject FishTankList;

#region Get One
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

    public GameObject TryGetSeaPlantPrefabById(int id)
    {
        return SeaPlantList.seaPlants.ToList().Find(s => s.id == id)?.prefab;
    }

    public GameObject TryGetRockPrefabById(int id)
    {
        return RockList.rocks.ToList().Find(s => s.id == id)?.prefab;
    }
#endregion

#region Get All
    public List<SeaPlantScriptableObjectStructure> GetSeaPlantList()
    {
        return SeaPlantList.seaPlants.ToList();
    }

    public List<RockScriptableObjectStructure> GetRockList()
    {
        return RockList.rocks.ToList();
    }

    public List<GameObject> GetSeaPlantPrefabList()
    {
        return SeaPlantList.seaPlants.ToList().ConvertAll(s => s.prefab);
    }

    public List<GameObject> GetRockPrefabList()
    {
        return RockList.rocks.ToList().ConvertAll(s => s.prefab);
    }
#endregion
}