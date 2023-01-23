using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ScriptableObjectManager : MonoBehaviour
{
    public EntityScriptableObject EntityList;

    public SeaObjectScriptableObject SeaObjectList;

    public FishTankScriptableObject FishTankList;

    private Dictionary<int, ItemType> seaObjectIdAndTypeDict = new Dictionary<int, ItemType>(); // USAGE: To easily get the item_type with type_id of <SeaObjectData> class.


    void Start()
    {
        // Init seaObjectIdAndTypeDict.
        foreach (var seaObjectSO in SeaObjectList.seaObjects)
        {
            seaObjectIdAndTypeDict[seaObjectSO.id] = seaObjectSO.type;
        }
    }

#region Get One
    public GameObject TryGetEntityPrefabById(int id)
    {
        foreach (var entity in EntityList.entities)
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
        foreach (var entity in EntityList.entities)
        {
            if (entity.id == id)
            {
                return entity;
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
        return SeaObjectList.seaObjects.ToList().Find(s => s.id == id)?.prefab;
    }

    public SeaObjectScriptableObjectStructure TryGetSeaObjectSOById(int id)
    {
        foreach (var seaObjectSO in SeaObjectList.seaObjects)
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

    public List<EntityScriptableObjectStructure> GetEntityList()
    {
        return EntityList.entities.ToList();
    }
    
    public List<SeaObjectScriptableObjectStructure> GetSeaObjectList()
    {
        return SeaObjectList.seaObjects.ToList();
    }

    public List<GameObject> GetSeaObjectPrefabList()
    {
        return SeaObjectList.seaObjects.ToList().ConvertAll(s => s.prefab);
    }
#endregion

    public ItemType GetSeaObjectItemTypeById(int id)
    {
        if (id < 0) {return ItemType.None;}
        return seaObjectIdAndTypeDict[id];
    }
}