using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StorePopupController : MonoBehaviour
{

    public StoreItemController itemPrefab;

    private PopupManager popupManager;
    private PopupType type = PopupType.StorePopup;
    private Button blockingButton;
    private StoreContent storeContent;

    private List<SeaPlantScriptableObjectStructure> seaPlantList;

    void Awake()
    {
        storeContent = transform.GetComponentInChildren<StoreContent>();
        blockingButton = transform.GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
    }

    void Start()
    {
        blockingButton.onClick.AddListener(OnBlockingPanelClick);
    }

#region Setup
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

    public void Select()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

#endregion

#region Button Event
    public void OnBuyButtonClick(int id)
    {
        print($"{id} - buy");
    }

    public void OnPreviewButtonClick(int id)
    {
        
    }

    // Outside of the current UI is clicked -> Close the current UI.
    private void OnBlockingPanelClick()
    {
        popupManager.ClosePopup(this.type);
    }
#endregion
}
