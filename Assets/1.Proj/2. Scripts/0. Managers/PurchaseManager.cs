using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fishn;
using Cysharp.Threading.Tasks;

public class PurchaseManager : MonoBehaviour
{
    public async UniTask<bool> TryPurchase(int type_id, ItemType type, int coral)
    {
        if (!Fishn.Wallet.HasEnough(coral)) return false;

        if (type == ItemType.Entity)
        {
            
        }

        if (type == ItemType.Plant || type == ItemType.Rock)
        {
            SeaObjectData newData = new SeaObjectData(-1, type_id, Vector3.zero, true);
            int id = await GameManager.instance.dataManager.AddSeaObject(newData);
            newData.id = id;
            GameManager.instance.viewSceneManager.fishTankManager.InstantiateSeaObject(newData);
        }

        // Fishn.Wallet.Use(coral);


        return true;
    }


}
