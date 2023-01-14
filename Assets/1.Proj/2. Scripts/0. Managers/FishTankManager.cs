using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTankManager : MonoBehaviour
{
    Transform myTransform;
    FishTankController fishtankController;
    void Awake()
    {
        myTransform = transform;
    }
    public void Setup(GameData gameData)
    {
        LoadFishTank(gameData.tank_id);
    }

    private void LoadFishTank(int id)
    {
        GameObject prefab = GameManager.instance.scriptableObjectManager.TryGetFishTankPrefabById(id);
        if (prefab)
        {
            GameObject fishTankObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, myTransform); // Tip 1 : Awake() of prefab is called when it instantiated.
            fishtankController = fishTankObject.GetComponent<FishTankController>();                          // Tip 2 : The script in instantiated Object and in Prefab are difference scripts.
            fishtankController.Setup();
        }
    }
}