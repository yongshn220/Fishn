using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityScriptableObject", menuName = "ScriptableObject/EntityScriptableObject")]
public class EntityScriptableObject : ScriptableObject
{
    public EntityScriptableObjectStructure[] Entity;
}
