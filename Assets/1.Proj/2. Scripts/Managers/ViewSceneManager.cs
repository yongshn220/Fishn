using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSceneManager : MonoBehaviour
{
    FishManager fishManager;
    PopupManager popupManager;

    void Awake()
    {
        fishManager = GetComponentInChildren<FishManager>();
        popupManager = GetComponentInChildren<PopupManager>();
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(!GameManager.instance.dataManager.isDataReady)
        {
            yield return new WaitForSeconds(1);
            //Wait and Try again
        }
        List<FishData> fishDataList = LoadFishDataList();
        
        fishManager.Setup(fishDataList);
        popupManager.Setup();
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
