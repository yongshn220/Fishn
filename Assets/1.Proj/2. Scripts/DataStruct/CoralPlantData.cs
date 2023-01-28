using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralPlantData
{
    public int id;
    public int type_id;
    public Vector3 position;

    public bool instantiated = false;

    public CoralPlantData(int id, int type_id, Vector3 position, bool instantiated)
    {
        this.id = id;
        this.type_id = type_id;
        this.position = position;
        this.instantiated = instantiated;
    }

    public CoralPlantData(CoralPlantData data)
    {
        this.id = data.id;
        this.type_id = data.type_id;
        this.position = data.position;
        this.instantiated = data.instantiated;
    }
}
