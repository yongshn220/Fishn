using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PurchaseManager : MonoBehaviour // to do: change class name -> TransactionManager
{
    public async UniTask<bool> TryPurchase(int type_id, ItemType type, int coral)
    {
        if (!Wallet.HasEnough(coral))
        {
            GameManager.instance.viewSceneManager.messageLogController.LogMessage("You don't have enough corals.");
            return false;
        }

        Wallet.Use(coral);

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

        if (type == ItemType.FishTank)
        {
            // If the selected tank id is the same as current -> fail to buy.
            if (GameManager.instance.viewSceneManager.fishTankManager.GetFishTankId() == type_id)
            {
                GameManager.instance.viewSceneManager.messageLogController.LogMessage("You already using the same tank size.");
                return false;
            }    
            await GameManager.instance.dataManager.AsyncSaveFishTankID(type_id);
            GameManager.instance.AsyncReload(); 
        }

        GameManager.instance.viewSceneManager.messageLogController.LogMessage("You successfully purchased an item.");
        return true;
    }

    public async UniTask<bool> TrySellEntity(int id, ItemType type, int coral)
    {
        if (type == ItemType.Entity)
        {
            await GameManager.instance.dataManager.RemoveEntity(id);
            GameManager.instance.viewSceneManager.fishManager.RemoveEntity(id);
            return true;
        }
        return false;
    }
}
