using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSceneManager : MonoBehaviour
{
    FishManager fishManager;
    void Awake()
    {
        fishManager = GetComponentInChildren<FishManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        List<FishData> fishDataList = LoadFishDataList();
        fishManager.Generate(fishDataList);
    }

    // Load Fish Data from DataManager.
    private List<FishData> LoadFishDataList()
    {
        if (GameManager.instance.dataManager.fishDataList != null)
        {
            return GameManager.instance.dataManager.fishDataList;
        }
        return new List<FishData>(); // TO DO : need to handle the error situation.
    }
}
