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
    private Button coralPlantButton;
    private Button fishTankButton;
    private StoreContent storeContent;

    private List<EntityScriptableObjectStructure> entitySOList;
    private List<SeaObjectScriptableObjectStructure> seaObjectSOList;
    private List<CoralScriptableObjectStructure> coralSOList;
    private List<FishTankScriptableObjectStructure> fishTankSOList;

    void Awake()
    {
        storeContent = transform.GetComponentInChildren<StoreContent>();
        blockingButton = transform.GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
        entityButton = transform.GetComponentInChildren<EntityButton>()?.GetComponent<Button>();
        plantButton = transform.GetComponentInChildren<PlantButton>()?.GetComponent<Button>();
        rockButton = transform.GetComponentInChildren<RockButton>()?.GetComponent<Button>();
        coralPlantButton = transform.GetComponentInChildren<CoralPlantButton>()?.GetComponent<Button>();
        fishTankButton = transform.GetComponentInChildren<FishTankButton>()?.GetComponent<Button>();
    }

    void Start()
    {
        blockingButton.onClick.AddListener(OnBlockingPanelClick);
        entityButton.onClick.AddListener(OnEntityButtonClick);
        plantButton.onClick.AddListener(OnPlantButtonClick);
        rockButton.onClick.AddListener(OnRockButtonClick);
        coralPlantButton.onClick.AddListener(OnCoralPlantButtonClick);
        fishTankButton.onClick.AddListener(OnFishTankButtonClick);
    }

#region IPopup
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        this.entitySOList = GameManager.instance.scriptableObjectManager.GetEntitySOList();
        this.seaObjectSOList = GameManager.instance.scriptableObjectManager.GetSeaObjectSOList();
        this.coralSOList = GameManager.instance.scriptableObjectManager.GetCoralPlantSOList();
        this.fishTankSOList = GameManager.instance.scriptableObjectManager.GetFishTankSOList();
        SetupItems();
    }

    public void Enable(int option, string data){}

    public void Disable()
    {
        
    }
#endregion

#region Setup
    private void SetupItems()
    {
        OnEntityButtonClick();
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
    // Called from Item Controller.
    public void OnBuyButtonClick(int id, ItemType type, int coral, string name)
    {
        print(id);
        print(type);
        popupManager.TryBuyItem(id, type, coral, name).Forget();
    }

    // Called from item controller.
    public void OnPreviewButtonClick(int id, ItemType type)
    {
        
    }

    private void OnEntityButtonClick()
    {
        ClearItemsInContent();

        foreach (var entityData in entitySOList)
        {
            if (entityData.id != -1)
            {
                StoreItemController itemCtrl = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, storeContent.transform);
                itemCtrl.Setup(this, entityData);
            }
        }
    }

    private void OnPlantButtonClick()
    {
        ClearItemsInContent();
        var seaPlantSOList = seaObjectSOList.FindAll((i) => i.type == ItemType.Plant);

        foreach (var plantSO in seaPlantSOList)
        {
            if (plantSO.id != -1)
            {
                StoreItemController itemCtrl = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, storeContent.transform);
                itemCtrl.Setup(this, plantSO);
            }
        }
    }

    private void OnRockButtonClick()
    {
        ClearItemsInContent();
        var rockSOList = seaObjectSOList.FindAll((i) => i.type == ItemType.Rock);

        foreach (var rockSO in rockSOList)
        {
            if (rockSO.id != -1)
            {
                StoreItemController itemCtrl = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, storeContent.transform);
                itemCtrl.Setup(this, rockSO);
            }
        }
    }

    private void OnCoralPlantButtonClick()
    {
        ClearItemsInContent();
        foreach (var coralSO in coralSOList)
        {
            if (coralSO.id != -1)
            {
                StoreItemController itemCtrl = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, storeContent.transform);
                itemCtrl.Setup(this, coralSO);
            }
        }
    }

    private void OnFishTankButtonClick()
    {
        ClearItemsInContent();
        // To do : filter tank that user already has.

        foreach (var fishTankSO in fishTankSOList)
        {
            if (fishTankSO.id != -1)
            {
                StoreItemController itemCtrl = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, storeContent.transform);
                itemCtrl.Setup(this, fishTankSO);
            }
        }
    }

    // Outside of the current UI is clicked -> Close the current UI.
    private void OnBlockingPanelClick()
    {
        popupManager.ClosePopup(this.type);
    }
#endregion
}
