using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityGrowthScriptableObject", menuName = "ScriptableObject/EntityGrowthScriptableObject")]
public class EntityGrowthScriptableObject : ScriptableObject
{
    public EntityGrowthScriptableObjectStructure[] entities;
}
