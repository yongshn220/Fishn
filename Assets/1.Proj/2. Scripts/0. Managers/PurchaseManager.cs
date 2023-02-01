using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PurchaseManager : MonoBehaviour // to do: change class name -> TransactionManager
{
    public async UniTask<bool> TryPurchase(int type_id, ItemType type, int coral)
    {
        if (!Wallet.HasEnough(coral)) return false;

        if (type == ItemType.Entity)
        {
            EntityData newData = new EntityData(-1, type_id, DateTime.Now, DateTime.Now, 0);
            newData.id = await GameManager.instance.dataManager.AddEntity(newData);
            GameManager.instance.viewSceneManager.fishManager.GenerateEntity(newData);
        }

        if (type == ItemType.Plant || type == ItemType.Rock)
        {
            SeaObjectData newData = new SeaObjectData(-1, type_id, Vector3.zero, true);
            newData.id = await GameManager.instance.dataManager.AddSeaObject(newData);
            GameManager.instance.viewSceneManager.fishTankManager.InstantiateSeaObject(newData);
        }

        if (type == ItemType.Coral)
        {
            CoralPlantData newData = new CoralPlantData(-1, type_id, Vector3.zero, true);
            newData.id = await GameManager.instance.dataManager.AddCoralPlant(newData);
            GameManager.instance.viewSceneManager.fishTankManager.InstantiateCoralPlant(newData);
        }

        // Fishn.Wallet.Use(coral);


        return true;
    }


}
