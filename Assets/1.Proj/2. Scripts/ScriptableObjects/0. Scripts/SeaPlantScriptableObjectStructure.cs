using System;
using UnityEngine;


public enum SeaObjectSize
{
    Small,
    Medium,
    Large,
}

[Serializable]
public class SeaPlantScriptableObjectStructure
{
    public int id;
    public string name;
    public SeaObjectSize size;
    public GameObject prefab;
}
