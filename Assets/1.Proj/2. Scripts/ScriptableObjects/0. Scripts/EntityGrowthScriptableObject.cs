using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityGrowthScriptableObject", menuName = "ScriptableObject/EntityGrowthScriptableObject")]
public class EntityGrowthScriptableObject : ScriptableObject
{
    public List<EntityGrowthScriptableObjectStructure> entityGrowths = new List<EntityGrowthScriptableObjectStructure>();
    
    public EntityGrowthScriptableObject()
    {
        // Clownfish
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Clownfish, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Clownfish, level:2, age:3, scale:0.4f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Clownfish, level:3, age:5, scale:0.5f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Clownfish, level:4, age:10, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Clownfish, level:5, age:15, scale:1.0f, maxFeed:1600));

        // Crab
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:5, age:12, scale:1.0f, maxFeed:1600)); 
    }
}
