using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPopupController : MonoBehaviour, IPopup
{
    public BagItemController itemPrefab;

    private PopupManager popupManager;
    private PopupType type = PopupType.BagPopup;
    private BagContent bagContent;
    private Button blockingButton;
    private Button entityButton;
    private Button plantButton;
    private Button rockButton;

    private List<EntityData> disabledEntityDataList = new List<EntityData>();
    private List<SeaObjectData> disabledSeaObjectDataList = new List<SeaObjectData>();

    void Awake()
    {
        bagContent = GetComponentInChildren<BagContent>();
        blockingButton = GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
        entityButton = GetComponentInChildren<EntityButton>()?.GetComponent<Button>();
        plantButton = GetComponentInChildren<PlantButton>()?.GetComponent<Button>();
        rockButton = GetComponentInChildren<RockButton>()?.GetComponent<Button>();
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
        this.disabledSeaObjectDataList = popupManager.GetDisabledSeaObjectDataList();
        // this.disabledEntityDataList = popupManager.GetDisabledEntityDataList();
        // event ++
    }

    public void Enable()
    {

    }

    public void Disable()
    {
        
    }
#endregion


#region Delegate Callback
    private void OnDisabledSeaObjectDataListUpdate()
    {
        this.disabledSeaObjectDataList = popupManager.GetDisabledSeaObjectDataList();
    }

    private void OnDisabledEntityDataListUpdate()
    {
        // this.disabledEntityDataList = popupManager.GetDisabledEntityDataList();
    }
#endregion

#region Button Event
    public void OnBuyButtonClick(int id)
    {

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

    private void OnEntityButtonClick()
    {
        ClearItemsInContent();

        foreach (var entityData in disabledEntityDataList)
        {
            if (entityData.id != -1)
            {
                BagItemController itemCtrl = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, bagContent.transform);
                var entitySO = GameManager.instance.scriptableObjectManager.TryGetEntitySOById(entityData.type_id);
                itemCtrl.Setup(this, entitySO, 1);
            }
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
