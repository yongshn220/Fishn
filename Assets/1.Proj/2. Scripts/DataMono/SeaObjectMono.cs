using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaObjectMono : MonoBehaviour
{
    public int id;
    public int type_id;
    public Vector3 position;
    public bool instantiated = false;

    public void Setup(SeaObjectData data)
    {
        this.id = data.id;
        this.type_id = data.type_id;
        this.position = data.position;
        this.instantiated = data.instantiated;
    }

    public SeaObjectData ToData()
    {
        return new SeaObjectData(this.id, this.type_id, this.position, this.instantiated);
    }
}
