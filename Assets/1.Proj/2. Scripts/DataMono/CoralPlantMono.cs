using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralPlantMono : MonoBehaviour
{
    public int id;
    public int type_id;
    public new string name;
    public int unitCoral;
    public Vector3 position;
    public bool instantiated = false;

    private bool _isFeeding = false; // Is any Entity eating this coral?
    public bool isFeeding { get { return _isFeeding; }}

    public void Setup(CoralPlantData data, CoralScriptableObjectStructure coralSO)
    {
        this.id = data.id;
        this.type_id = data.type_id;
        this.position = data.position;
        this.instantiated = data.instantiated;

        this.name = coralSO.name;
        this.unitCoral = coralSO.unitCoral;
    }

    public CoralPlantData ToData()
    {
        return new CoralPlantData(this.id, this.type_id, this.position, this.instantiated);
    }

    public void StartFeeding()
    {
        _isFeeding = true;
    }

    public void FinishFeeding()
    {  
        _isFeeding = false;
    }
}
