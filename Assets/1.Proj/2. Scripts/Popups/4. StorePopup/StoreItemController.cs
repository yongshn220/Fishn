using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemController : MonoBehaviour
{
    public StorePopupController popupController;

    private int id;
    private string name;
    private ItemType type;
    private int coral;

    public Image itemImage;
    public TMP_Text coralValue;
    public TMP_Text itemName;
    public Button buyButton;
    public Button previewButton;

    void Start()
    {
        buyButton.onClick.AddListener(OnBuyButtonClick);
        previewButton.onClick.AddListener(OnPreviewButtonClick);
    }

    public void Setup(StorePopupController popupController, SeaObjectScriptableObjectStructure seaObject)
    {
        this.popupController = popupController;
        this.id = seaObject.id;
        this.name = seaObject.name;
        this.type = seaObject.type;
        this.coral = seaObject.coral;
        this.coralValue.text = this.coral.ToString();
        this.itemName.text = seaObject.name;
        this.itemImage.sprite = seaObject.sprite;
    }

    public void Setup(StorePopupController popupController, EntityScriptableObjectStructure entity)
    {
        this.popupController = popupController;
        this.id = entity.id;
        this.name = entity.name;
        this.type = entity.type;
        this.coral = entity.coral;
        this.coralValue.text = this.coral.ToString();
        this.itemName.text = entity.name;
        this.itemImage.sprite = entity.sprite;
    }

    public void Setup(StorePopupController popupController, CoralScriptableObjectStructure coralPlant)
    {
        this.popupController = popupController;
        this.id = coralPlant.id;
        this.name = coralPlant.name;
        this.type = coralPlant.type;
        this.coral = coralPlant.coral;
        this.coralValue.text = this.coral.ToString();
        this.itemName.text = coralPlant.name + $"\n+{coralPlant.unitCoral / 10}";
        this.itemImage.sprite = coralPlant.sprite;
    }

    public void Setup(StorePopupController popupController, FishTankScriptableObjectStructure fishTank)
    {
        this.popupController = popupController;
        this.id = fishTank.id;
        this.name = fishTank.name;
        this.type = fishTank.type;
        this.coral = fishTank.coral;
        this.coralValue.text = this.coral.ToString();
        this.itemName.text = fishTank.name;
        this.itemImage.sprite = fishTank.sprite;
    }

    private void OnBuyButtonClick()
    {
        popupController.OnBuyButtonClick(id, type, coral, name);
    }

    private void OnPreviewButtonClick()
    {
        popupController.OnPreviewButtonClick(id, type);
    }
}
