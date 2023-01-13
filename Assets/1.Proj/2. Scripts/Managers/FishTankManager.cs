using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTankManager : MonoBehaviour
{
    Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }
    public void Setup(GameData gameData)
    {
        LoadFishTank(gameData.id);
    }

    private void LoadFishTank(int id)
    {
        GameObject prefab = GameManager.instance.scriptableObjectManager.TryGetFishTankPrefabById(id);
        if (prefab)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity, myTransform);
        }
    }

    private void LoadStructures()
    {

    }
}
