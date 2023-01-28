using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPopupController : MonoBehaviour, IPopup
{
    public BagItemController itemPrefab;

    private PopupManager popupManager;
    private PopupType type = PopupType.BagPopup;
    private ItemType currentMenuType = ItemType.None;
    private BagContent bagContent;
    private Button blockingButton; 
    private Button plantMenuButton;
    private Button rockMenuButton;

    private List<SeaObjectData> disabledSeaObjectDataList = new List<SeaObjectData>();
    // private List<>

    void Awake()
    {
        bagContent = GetComponentInChildren<BagContent>();
        blockingButton = GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
        plantMenuButton = GetComponentInChildren<PlantButton>()?.GetComponent<Button>();
        rockMenuButton = GetComponentInChildren<RockButton>()?.GetComponent<Button>();
    }

    void Start()
    {
        blockingButton.onClick.AddListener(OnBlockingPanelClick);
        plantMenuButton.onClick.AddListener(OnPlantButtonClick);
        rockMenuButton.onClick.AddListener(OnRockButtonClick);
    }

#region IPopup
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        this.disabledSeaObjectDataList = popupManager.GetDisabledSeaObjectDataList(); // disabledObject -> put in Bag.
        FishTankController.OnSeaObjectUpdate += OnDisabledSeaObjectDataListUpdate;    // Delegate(Action)
        OnPlantButtonClick();
    }

    public void Enable()
    {
        OnPlantButtonClick();
    }

    public void Disable(){}
#endregion


#region Delegate Callback
    private void OnDisabledSeaObjectDataListUpdate()
    {
        this.disabledSeaObjectDataList = popupManager.GetDisabledSeaObjectDataList();
        if (currentMenuType == ItemType.Plant) OnPlantButtonClick(); // Load Plant content
        if (currentMenuType == ItemType.Rock) OnRockButtonClick(); // Load Rock content
    }
#endregion

#region Button Event
    public void OnUseButtonClick(int type_id)
    {
        popupManager.LoadSeaObjectFromBag(type_id);
    }

    // Outside of the current UI is clicked -> Close the current UI.
    private void OnBlockingPanelClick()
    {
        popupManager.ClosePopup(this.type);
    }

    private void ClearItemsInContent()
    {
        foreach (Transform child in bagContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnPlantButtonClick()
    {
        ClearItemsInContent();

        // Select only plant data;
        List<SeaObjectData> seaPlantDataList = disabledSeaObjectDataList.FindAll((data) => {
            return GameManager.instance.scriptableObjectManager.GetSeaObjectItemTypeById(data.type_id) == ItemType.Plant;
        });

        // Count each data into dictionary.
        Dictionary<SeaObjectData, int> seaPlantDataDict = GetCountSeaObjectDataDict(seaPlantDataList);

        // Instantiate Item.
        foreach (var seaObjectPair in seaPlantDataDict)
        {
            if (seaObjectPair.Key.id == -1) return;

            BagItemController itemCtrl = Instantiate(itemPrefab, bagContent.transform);
            var seaPlantSO = GameManager.instance.scriptableObjectManager.TryGetSeaObjectSOById(seaObjectPair.Key.type_id);
            if (seaPlantSO != null)
            {
                itemCtrl.Setup(this, seaPlantSO, seaObjectPair.Value);
            }
        }
        currentMenuType = ItemType.Plant;
    }

    private void OnRockButtonClick()
    {
        ClearItemsInContent();

        // Select only Rock data.
        List<SeaObjectData> rockDataList = disabledSeaObjectDataList.FindAll((data) => {
            return GameManager.instance.scriptableObjectManager.GetSeaObjectItemTypeById(data.type_id) == ItemType.Rock;
        });

        // Count each data into dictionary.
        Dictionary<SeaObjectData, int> seaPlantDataDict = GetCountSeaObjectDataDict(rockDataList);

        // Instantiate Item.
        foreach (var seaObjectPair in seaPlantDataDict)
        {
            if (seaObjectPair.Key.id == -1) return;

            BagItemController itemCtrl = Instantiate(itemPrefab, bagContent.transform);
            var seaPlantSO = GameManager.instance.scriptableObjectManager.TryGetSeaObjectSOById(seaObjectPair.Key.type_id);
            if (seaPlantSO != null)
            {
                itemCtrl.Setup(this, seaPlantSO, seaObjectPair.Value);
            }
        }
        currentMenuType = ItemType.Rock;
    }

    // Count each data into dictionary.
    private Dictionary<SeaObjectData, int> GetCountSeaObjectDataDict(List<SeaObjectData> seaObjectDataList)
    {
        Dictionary<SeaObjectData, int> resultDict = new Dictionary<SeaObjectData, int>();
        foreach (var seaPlantData in seaObjectDataList)
        {
            if (resultDict.ContainsKey(seaPlantData))
            {
                resultDict[seaPlantData] += 1;
            }
            else
            {
                resultDict[seaPlantData] = 1;
            }
        }
        return resultDict;
    }
    #endregion
}
