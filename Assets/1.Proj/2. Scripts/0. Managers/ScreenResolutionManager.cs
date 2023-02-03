using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

struct Resolution
{
    public int x;
    public int y;
}

public enum ScreenType
{
    Size1x1 = 1,
    Size2x1 = 2,
    Size3x1 = 3,
    Size3x2 = 4,
    Size4x2 = 5,
}

public class ScreenResolutionManager : MonoBehaviour
{
    private int unitInPixel = 600; // Size1x1 -> 600px X 600px
    public ScreenType currScreenType = ScreenType.Size1x1;

    public void Setup()
    {
        int tank_id = GameManager.instance.dataManager.GetUserTankId();
        ChangeResolution(tank_id);
    }

    public void ChangeResolution(int tank_id)
    {   
        ScreenType type = (ScreenType)Enum.ToObject(typeof(ScreenType), tank_id); 
        if (type == currScreenType) return;
        
        currScreenType = type;
        Resolution resolution = GetResolution(currScreenType); // Convert ScreenType(enum) -> Resolution(struct)
        Screen.SetResolution(resolution.x, resolution.y, false);
        Debug.Log("Screen Resolution Updated : " + currScreenType);
    }

    private Resolution GetResolution(ScreenType type)
    { 
        switch(type)
        {
            case ScreenType.Size1x1 : return new Resolution {x = unitInPixel, y = unitInPixel};
            case ScreenType.Size2x1 : return new Resolution {x = unitInPixel * 2, y = unitInPixel};
            case ScreenType.Size3x1 : return new Resolution {x = unitInPixel * 3, y = unitInPixel * 1};
            case ScreenType.Size3x2 : return new Resolution {x = unitInPixel * 3, y = unitInPixel * 2};
            case ScreenType.Size4x2 : return new Resolution {x = unitInPixel * 4, y = unitInPixel * 2};
            default : return new Resolution {x = unitInPixel, y = unitInPixel};
        }
    }
}
