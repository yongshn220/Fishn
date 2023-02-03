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
    Basic2x2 = 1, // 800x800
    Basic3x2 = 2, // 1200x800
    Basic4x2 = 3, // 1600x800
    Basic3x3 = 4, // 1200x1200
    Basic4x3 = 5, // 1600x1200
}

public class ScreenResolutionManager : MonoBehaviour
{
    private int unitInPixel = 400; // Size1x1 -> 400px X 400px
    public ScreenType currScreenType = ScreenType.Basic2x2;

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
            case ScreenType.Basic2x2 : return new Resolution {x = unitInPixel * 2, y = unitInPixel * 2};
            case ScreenType.Basic3x2 : return new Resolution {x = unitInPixel * 3, y = unitInPixel * 2};
            case ScreenType.Basic4x2 : return new Resolution {x = unitInPixel * 4, y = unitInPixel * 2};
            case ScreenType.Basic3x3 : return new Resolution {x = unitInPixel * 3, y = unitInPixel * 3};
            case ScreenType.Basic4x3 : return new Resolution {x = unitInPixel * 4, y = unitInPixel * 3};
            default : return new Resolution {x = unitInPixel, y = unitInPixel};
        }
    }
}
