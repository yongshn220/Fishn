using UnityEngine;


[CreateAssetMenu(fileName = "SeaObjectScriptableObject", menuName = "ScriptableObject/SeaObjectScriptableObject")]
public class SeaObjectScriptableObject : ScriptableObject
{
    public SeaObjectScriptableObjectStructure[] seaObjects;
}
