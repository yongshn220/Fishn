
using System;
using UnityEngine;

[Serializable]
public class CoralScriptableObjectStructure
{
    public int id;
    public string name;
    public int coral;
    public int unitPrice;
    public ItemType type = ItemType.Coral;
    public SeaObjectSize size;
    public GameObject prefab;
}
