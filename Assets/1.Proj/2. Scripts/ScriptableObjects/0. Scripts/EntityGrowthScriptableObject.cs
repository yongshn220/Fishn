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
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Clownfish, level:5, age:9999, scale:1.0f, maxFeed:1600));

        // Crab
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:5, age:12, scale:1.0f, maxFeed:1600)); 
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Crab, level:5, age:9999, scale:1.0f, maxFeed:1600));

        // Dolphin
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Dolphin, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Dolphin, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Dolphin, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Dolphin, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Dolphin, level:5, age:12, scale:1.0f, maxFeed:1600)); 
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Dolphin, level:5, age:9999, scale:1.0f, maxFeed:1600));

        // Lobster
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Lobster, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Lobster, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Lobster, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Lobster, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Lobster, level:5, age:12, scale:1.0f, maxFeed:1600)); 
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Lobster, level:5, age:9999, scale:1.0f, maxFeed:1600));

        // Orca
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Orca, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Orca, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Orca, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Orca, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Orca, level:5, age:12, scale:1.0f, maxFeed:1600)); 
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Orca, level:5, age:9999, scale:1.0f, maxFeed:1600));

        // Pelican
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Pelican, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Pelican, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Pelican, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Pelican, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Pelican, level:5, age:12, scale:1.0f, maxFeed:1600)); 
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Pelican, level:5, age:9999, scale:1.0f, maxFeed:1600));

        // SeaHorse
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaHorse, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaHorse, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaHorse, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaHorse, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaHorse, level:5, age:12, scale:1.0f, maxFeed:1600)); 
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaHorse, level:5, age:9999, scale:1.0f, maxFeed:1600));

        // SeaOtter
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaOtter, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaOtter, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaOtter, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaOtter, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaOtter, level:5, age:12, scale:1.0f, maxFeed:1600)); 
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.SeaOtter, level:5, age:9999, scale:1.0f, maxFeed:1600));

        // Squid
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Squid, level:1, age:0, scale:0.3f, maxFeed:100));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Squid, level:2, age:3, scale:0.5f, maxFeed:200));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Squid, level:3, age:5, scale:0.6f, maxFeed:400));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Squid, level:4, age:8, scale:0.8f, maxFeed:800));
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Squid, level:5, age:12, scale:1.0f, maxFeed:1600)); 
        entityGrowths.Add(new EntityGrowthScriptableObjectStructure(EntityType.Squid, level:5, age:9999, scale:1.0f, maxFeed:1600));
    }
}
