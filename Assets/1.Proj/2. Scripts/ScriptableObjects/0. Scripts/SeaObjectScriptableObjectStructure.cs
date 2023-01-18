using System;
using UnityEngine;


public enum SeaObjectSize
{
    Small,
    Medium,
    Large,
}

public enum SeaObjectType
{
    Plant,
    Rock,
}

[Serializable]
public class SeaObjectScriptableObjectStructure
{
    public int id;
    public string name;
    public int coral;
    public SeaObjectType type;
    public SeaObjectSize size;
    public GameObject prefab;
}
