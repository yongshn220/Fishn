using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPopupController : MonoBehaviour, IPopup
{
    private PopupManager popupManager;
    private PopupType type = PopupType.BagPopup;
    private BagContent bagContent;
    private Button blockingButton;
    private Button entityButton;
    private Button plantButton;
    private Button rockButton;

    private List<EntityMono> entityMonoList = new List<EntityMono>();
    private List<SeaObjectMono> seaObjectMonoList = new List<SeaObjectMono>();

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
    }

    public void Enable()
    {

    }

    public void Disable()
    {
        
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
        
    }

    private void OnRockButtonClick()
    {
        
    }

    #endregion
}
