using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DelegateManager : MonoBehaviour
{
    
    public static event Action<List<EntityMono>> OnEntityMonoUpdate;

    public static event Action<List<SeaObjectData>> OnDisabledSeaObjectUpdate;
    public static event Action<List<CoralPlantData>> OnDisabledCoralPlantUpdate;

    public void InvokeOnEntityMonoUpdate()
    {
        // To do :
    }
}
