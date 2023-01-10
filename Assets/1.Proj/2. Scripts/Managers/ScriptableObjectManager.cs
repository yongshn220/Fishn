using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptableObjectManager : MonoBehaviour
{
    public EntityScriptableObject EntityList;

    public GameObject getEntityPrefabById(int id)
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
}

