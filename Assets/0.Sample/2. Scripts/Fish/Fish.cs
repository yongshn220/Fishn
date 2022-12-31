using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishnData;

public class Fish : MonoBehaviour
{
    [SerializeField] 
    private FishLevel level;
    [SerializeField] 
    private FishType type;
    [SerializeField] 
    private FishColor color;

    
    void init()
    {
        // if first run
        level = FishLevel.egg;
        type = FishType.idle;
        color = FishColor.idle;
    }
}
