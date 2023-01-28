using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralPlantMono : MonoBehaviour
{
    public int id;
    public int type_id;
    public new string name;
    public int unitPrice;
    public Vector3 position;
    public bool instantiated = false;

    public void Setup(CoralPlantData data, CoralScriptableObjectStructure coralSO)
    {
        this.id = data.id;
        this.type_id = data.type_id;
        this.position = data.position;
        this.instantiated = data.instantiated;

        this.name = coralSO.name;
        this.unitPrice = coralSO.unitPrice;
    }

    public CoralPlantData ToData()
    {
        return new CoralPlantData(this.id, this.type_id, this.position, this.instantiated);
    }
}
