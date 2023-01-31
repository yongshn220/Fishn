using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DelegateManager : MonoBehaviour
{
    
    public static event Action<List<EntityMono>> OnEntityMonoUpdate;
    public static event Action<List<SeaObjectData>> OnDisabledSeaObjectUpdate;
    public static event Action<List<CoralPlantData>> OnDisabledCoralPlantUpdate;

    public static void InvokeOnEntityMonoUpdate(List<EntityMono> monoList)
    {
        OnEntityMonoUpdate.Invoke(monoList);
    }

    public static void InvokeOnDisabledSeaObjectUpdate(List<SeaObjectData> dataList)
    {
        OnDisabledSeaObjectUpdate.Invoke(dataList);
    }

    public static void InvokeOnDisabledCoralPlantUpdate(List<CoralPlantData> dataList)
    {
        OnDisabledCoralPlantUpdate.Invoke(dataList);
    }
}
