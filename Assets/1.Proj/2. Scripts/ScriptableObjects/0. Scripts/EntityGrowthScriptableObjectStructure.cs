using System;
using UnityEngine;

public enum EntityType
{
    Clownfish = 1,
    Crab = 2,
    Dolphin = 3,
    Lobster = 4,
    Orca = 5,
    Pelican = 6,
    SeaHorse = 7,
    SeaOtter = 8,
    Squid = 9,
}

[Serializable]
public class EntityGrowthScriptableObjectStructure
{
    public EntityType type;
    public int level;
    public int age;
    public float scale;
    public int maxFeed;

    public EntityGrowthScriptableObjectStructure(EntityType type, int level, int age, float scale, int maxFeed)
    {
        this.type = type;
        this.level = level;
        this.age = age;
        this.scale = scale;
        this.maxFeed = maxFeed;
    }
}
