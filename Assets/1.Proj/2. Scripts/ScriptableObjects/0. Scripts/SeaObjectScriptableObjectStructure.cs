using System;
using UnityEngine;


public enum SeaObjectSize
{
    Small,
    Medium,
    Large,
}

public enum ItemType
{
    None,
    Entity,
    Plant,
    Rock,
    Coral,
    FishTank,
}

[Serializable]
public class SeaObjectScriptableObjectStructure
{
    public int id;
    public string name;
    public int coral;
    public ItemType type;
    public SeaObjectSize size;
    public GameObject prefab;
    public Sprite sprite;
}
