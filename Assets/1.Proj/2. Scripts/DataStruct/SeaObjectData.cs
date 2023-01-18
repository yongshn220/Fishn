using UnityEngine;

public class SeaObjectData : MonoBehaviour
{
    public int id;
    public int type_id;
    public Vector3 position;

    public bool isInstantiated = false;

    public void Setup(int id, int type_id, Vector3 position)
    {
        this.id = id;
        this.type_id = type_id;
        this.position = position;
    }

    public void Setup(SeaObjectData data)
    {
        this.id = data.id;
        this.type_id = data.type_id;
        this.position = data.position;
    }

    public override string ToString()
    {
        return $"id = {id}, position = {position}";
    }
}

