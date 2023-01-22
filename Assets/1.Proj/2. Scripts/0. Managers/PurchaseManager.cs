using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fishn;

public class PurchaseManager : MonoBehaviour
{
    public bool TryPurchase(int id, ItemType type, int coral)
    {
        if (!Fishn.Wallet.HasEnough(coral)) return false;

        if (type != ItemType.Entity)
        {
            GameManager.instance.dataManager.AddSeaObject(id);
            // GameManager.instance.viewSceneManager.fishTankManager.AddSeaObject();
        }
        // Fishn.Wallet.Use(coral);

        // GameManager.instance.dataManager

        return true;
    }


}
