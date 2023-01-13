using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishTankScriptableObject", menuName = "ScriptableObject/FishTankScriptableObject")]
public class FishTankScriptableObject : ScriptableObject
{
    public FishTankScriptableObjectStructure[] fishTanks;
}
