using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DelegateManager : MonoBehaviour
{
    public static event Action OnCoralUpdate;
    public static event Action<EntityMono> OnEntityMonoUpdate;
    public static event Action<List<SeaObjectData>> OnDisabledSeaObjectUpdate;
    public static event Action<List<CoralPlantData>> OnDisabledCoralPlantUpdate;

    public static void InvokeOnCoralUpdate() => OnCoralUpdate.Invoke();
    public static void InvokeOnEntityMonoUpdate(EntityMono mono) => OnEntityMonoUpdate.Invoke(mono);
    public static void InvokeOnDisabledSeaObjectUpdate(List<SeaObjectData> dataList) => OnDisabledSeaObjectUpdate.Invoke(dataList);
    public static void InvokeOnDisabledCoralPlantUpdate(List<CoralPlantData> dataList) => OnDisabledCoralPlantUpdate.Invoke(dataList);
}
