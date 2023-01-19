using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StorePopupController : MonoBehaviour, IPopup
{
    public StoreItemController itemPrefab;

    private PopupManager popupManager;
    private PopupType type = PopupType.StorePopup;
    private Button blockingButton;
    private Button entityButton;
    private Button plantButton;
    private Button rockButton;
    private StoreContent storeContent;

    private List<SeaObjectScriptableObjectStructure> seaObjectItemList;

    void Awake()
    {
        storeContent = transform.GetComponentInChildren<StoreContent>();
        blockingButton = transform.GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
        entityButton = transform.GetComponentInChildren<EntityButton>()?.GetComponent<Button>();
        plantButton = transform.GetComponentInChildren<PlantButton>()?.GetComponent<Button>();
        rockButton = transform.GetComponentInChildren<RockButton>()?.GetComponent<Button>();
    }

    void Start()
    {
        blockingButton.onClick.AddListener(OnBlockingPanelClick);
        entityButton.onClick.AddListener(OnEntityButtonClick);
        plantButton.onClick.AddListener(OnPlantButtonClick);
        rockButton.onClick.AddListener(OnRockButtonClick);
    }

#region IPopup
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        this.seaObjectItemList = GameManager.instance.scriptableObjectManager.GetSeaObjectList();
        SetupItems();
    }

    public void Enable()
    {

    }

    public void Disable()
    {
        
    }
#endregion

#region Setup
    private void SetupItems()
    {
        OnPlantButtonClick();
    }

    private void ClearItemsInContent()
    {
        foreach (Transform child in storeContent.transform)
        {
            Destroy(child.gameObject);
        }
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

    public void OnEntityButtonClick()
    {
        //
    }

    public void OnPlantButtonClick()
    {
        print("Plant");
        ClearItemsInContent();
        var seaPlantItemList = seaObjectItemList.FindAll((i) => i.type == SeaObjectType.Plant);

        foreach (var itemData in seaPlantItemList)
        {
            itemPrefab.Setup(this, itemData);
            Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, storeContent.transform);
        }
    }

    public void OnRockButtonClick()
    {
        print("Rock");
        ClearItemsInContent();
        var rockItemList = seaObjectItemList.FindAll((i) => i.type == SeaObjectType.Rock);

        foreach (var itemData in rockItemList)
        {
            itemPrefab.Setup(this, itemData);
            Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, storeContent.transform);
        }
    }

    // Outside of the current UI is clicked -> Close the current UI.
    private void OnBlockingPanelClick()
    {
        popupManager.ClosePopup(this.type);
    }
#endregion
}
