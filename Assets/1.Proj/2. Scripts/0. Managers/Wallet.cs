using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class Wallet
{
    private static int _coral;
    public static int coral { get{return _coral;}}

    private static bool isDelayOn;
    private static int saveDelayTime = 5; // Wallet saved to DB after <saveDelayTime>.

    public static void SetCoral(int coral)
    {
        if (coral >= 0)
        {
            _coral = coral;
        }
    }

    public static bool HasEnough(int amount)
    {
        return (coral >= amount);
    }

    public static bool Use(int amount)
    {
        if (HasEnough(amount))
        {
            _coral = _coral - amount;
            Save();
            return true;
        }
        return false;
    }

    public static void Earn(int amount)
    {
        if (amount > 0)
        {
            _coral += amount;
        }
        Save();
        DelegateManager.InvokeOnCoralUpdate();
    }

    // This function Frequently Called. 
    public static void FixedEarn(int amount)
    {  
        Earn(amount);

        if (!isDelayOn) 
        {
            isDelayOn = true;
            Save(saveDelayTime);
        }
    }

    private async static void Save(int delay = 0)
    {
        await Task.Delay(delay * 1000);
        GameManager.instance.dataManager.SaveCoral(coral);
        isDelayOn = false;
    }
}
