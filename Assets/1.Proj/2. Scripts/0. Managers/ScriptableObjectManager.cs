using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ScriptableObjectManager : MonoBehaviour
{
    public EntityScriptableObject EntitySOList;
    public EntityGrowthScriptableObject EntityGrowthSOList;
    public SeaObjectScriptableObject SeaObjectSOList;
    public FishTankScriptableObject FishTankSOList;
    public CoralScriptableObject CoralPlantSOList;

    private Dictionary<int, ItemType> seaObjectIdAndTypeDict = new Dictionary<int, ItemType>(); // USAGE: To easily get the item_type with type_id of <SeaObjectData> class.


    void Start()
    {
        // Init seaObjectIdAndTypeDict.
        foreach (var seaObjectSO in SeaObjectSOList.seaObjects)
        {
            seaObjectIdAndTypeDict[seaObjectSO.id] = seaObjectSO.type;
        }
        Setup();
    }
    
    void Setup()
    {
        EntityGrowthSOList = new EntityGrowthScriptableObject();
    }

#region Get One
    public GameObject TryGetEntityPrefabById(int id)
    {
        foreach (var entity in EntitySOList.entities)
        {
            if (entity.id == id)
            {
                return entity.prefab;
            }
        }
        return null;
    }

    public EntityScriptableObjectStructure TryGetEntitySOById(int id)
    {
        foreach (var entity in EntitySOList.entities)
        {
            if (entity.id == id)
            {
                return entity;
            }
        }
        return null;
    }

    public EntityGrowthScriptableObjectStructure TryGetEntityGrowthSOByData(EntityData data)
    {
        foreach (var entityGrowth in EntityGrowthSOList.entityGrowths)
        {
            if (entityGrowth.type == (EntityType) Enum.ToObject(typeof(EntityType), data.type_id) && entityGrowth.age >= data.born_datetime.GetDayPassedFromNow()) 
            {
                return entityGrowth;
            }
        }
        return null;
    }

    public CoralScriptableObjectStructure TryGetCoralPlantSOById(int id)
    {
        foreach (var coralSO in CoralPlantSOList.corals)
        {
            if (coralSO.id == id)
            {
                return coralSO;
            }
        }
        return null;
    }

    public GameObject TryGetFishTankPrefabById(int id)
    {
        foreach (var tank in FishTankSOList.fishTanks)
        {
            if (tank.id == id)
            {
                return tank.prefab;
            }
        }
        return null;
    }

    public GameObject TryGetSeaObjectPrefabById(int id)
    {
        return SeaObjectSOList.seaObjects.ToList().Find(s => s.id == id)?.prefab;
    }

    public GameObject TryGetCoralPlantPrefabById(int id)
    {
        return CoralPlantSOList.corals.ToList().Find(s => s.id == id)?.prefab;
    }

    public SeaObjectScriptableObjectStructure TryGetSeaObjectSOById(int id)
    {
        foreach (var seaObjectSO in SeaObjectSOList.seaObjects)
        {
            if (seaObjectSO.id == id)
            {
                return seaObjectSO;
            }
        }
        return null;
    }

#endregion

#region Get All

    public List<EntityScriptableObjectStructure> GetEntitySOList()
    {
        return EntitySOList.entities.ToList();
    }
    
    public List<SeaObjectScriptableObjectStructure> GetSeaObjectSOList()
    {
        return SeaObjectSOList.seaObjects.ToList();
    }

    public List<CoralScriptableObjectStructure> GetCoralPlantSOList()
    {
        return CoralPlantSOList.corals.ToList();
    }

    public List<FishTankScriptableObjectStructure> GetFishTankSOList()
    {
        return FishTankSOList.fishTanks.ToList();
    }

    public List<GameObject> GetSeaObjectPrefabList()
    {
        return SeaObjectSOList.seaObjects.ToList().ConvertAll(s => s.prefab);
    }

#endregion

    public ItemType GetSeaObjectItemTypeById(int id)
    {
        if (id < 0) { return ItemType.None; }
        return seaObjectIdAndTypeDict[id];
    }
}