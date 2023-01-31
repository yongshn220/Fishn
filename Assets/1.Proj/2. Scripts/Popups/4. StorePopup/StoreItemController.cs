using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemController : MonoBehaviour
{
    public StorePopupController popupController;

    private int id;
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
        this.type = seaObject.type;
        this.coral = seaObject.coral;
        this.coralValue.text = this.coral.ToString();
        this.itemName.text = seaObject.name;
    }

    public void Setup(StorePopupController popupController, EntityScriptableObjectStructure entity)
    {
        this.popupController = popupController;
        this.id = entity.id;
        this.type = entity.type;
        this.coral = entity.coral;
        this.coralValue.text = this.coral.ToString();
        this.itemName.text = entity.name;
    }

    public void Setup(StorePopupController popupController, CoralScriptableObjectStructure coralPlant)
    {
        this.popupController = popupController;
        this.id = coralPlant.id;
        this.type = coralPlant.type;
        this.coral = coralPlant.coral;
        this.coralValue.text = this.coral.ToString();
        this.itemName.text = coralPlant.name + $" [{coralPlant.unitCoral}]";
    }

    private void OnBuyButtonClick()
    {
        popupController.OnBuyButtonClick(id, type, coral);
    }

    private void OnPreviewButtonClick()
    {
        popupController.OnPreviewButtonClick(id, type);
    }
}
