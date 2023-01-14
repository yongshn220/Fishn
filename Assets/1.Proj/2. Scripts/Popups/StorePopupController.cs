using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePopupController : MonoBehaviour
{
    private PopupManager popupManager;
    public StoreItemController itemPrefab;

    StoreContent storeContent;
    List<SeaPlantScriptableObjectStructure> seaPlantList;

    void Awake()
    {
        storeContent = transform.GetComponentInChildren<StoreContent>();
    }

    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        this.seaPlantList = GameManager.instance.scriptableObjectManager.GetSeaPlantList();
        SetupItems();
    }

    private void SetupItems()
    {
        ClearItemsInContent();
        print(seaPlantList.Count);
        foreach (var plantData in seaPlantList)
        {
            itemPrefab.Setup(this, plantData);
            Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, storeContent.transform);
        }
    }

    private void ClearItemsInContent()
    {
        foreach (Transform child in storeContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnBuyButtonClick(int id)
    {
        print($"{id} - buy");
    }

    public void OnPreviewButtonClick(int id)
    {
        
    }
}
