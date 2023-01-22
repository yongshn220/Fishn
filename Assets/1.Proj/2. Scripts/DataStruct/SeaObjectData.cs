using UnityEngine;

public class SeaObjectData
{
    public int id;
    public int type_id;
    public Vector3 position;

    public bool instantiated = false;

    public SeaObjectData(int id, int type_id, Vector3 position, bool instantiated)
    {
        this.id = id;
        this.type_id = type_id;
        this.position = position;
        this.instantiated = instantiated;
    }

    public SeaObjectData(SeaObjectData data)
    {
        this.id = data.id;
        this.type_id = data.type_id;
        this.position = data.position;
        this.instantiated = data.instantiated;
    }

    public override string ToString()
    {
        return $"id = {id}, position = {position}";
    }
}

