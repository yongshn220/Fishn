using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    clownFish,
    crab,
    dolphin,
    lobster,
    orca,
    pelican,
    seahorse,
    seaOtter,
    squid,
}

public class PrefabManager : MonoBehaviour
{
    public GameObject clownFish;
    public GameObject crab;
    public GameObject dolphin;
    public GameObject lobster;
    public GameObject orca;
    public GameObject pelican;
    public GameObject seahorse;
    public GameObject seaOtter;
    public GameObject squid;

    public GameObject getEntityPrefabById(int id)
    {
        switch(id)
        {
            case 1 : return clownFish;
            case 2 : return crab;
            case 3 : return dolphin;
            case 4 : return lobster;
            case 5 : return orca;
            case 6 : return pelican;
            case 7 : return seahorse;
            case 8 : return seaOtter;
            case 9 : return squid;
            default : return null;
        }
    }
}

