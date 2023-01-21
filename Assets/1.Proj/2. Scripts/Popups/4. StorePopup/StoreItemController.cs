using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemController : MonoBehaviour
{
    public StorePopupController popupController;

    public int id;
    public ItemType type;
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
        this.coralValue.text = seaObject.coral.ToString();
        this.itemName.text = seaObject.name;
    }

    public void Setup(StorePopupController popupController, EntityScriptableObjectStructure entity)
    {
        this.popupController = popupController;
        this.id = entity.id;
        this.type = entity.type;
        this.coralValue.text = entity.coral.ToString();
        this.itemName.text = entity.name;
    }

    private void OnBuyButtonClick()
    {
        popupController.OnBuyButtonClick(id, type);
    }

    private void OnPreviewButtonClick()
    {
        popupController.OnPreviewButtonClick(id, type);
    }
}
