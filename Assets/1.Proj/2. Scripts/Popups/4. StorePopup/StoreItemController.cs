using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemController : MonoBehaviour
{
    public StorePopupController popupController;

    public int id;
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

    public void Setup(StorePopupController popupController, SeaObjectScriptableObjectStructure seaPlant)
    {
        this.popupController = popupController;
        this.id = seaPlant.id;
        this.coralValue.text = seaPlant.coral.ToString();
        this.itemName.text = seaPlant.name;
    }

    private void OnBuyButtonClick()
    {
        popupController.OnBuyButtonClick(id);
    }

    private void OnPreviewButtonClick()
    {
        popupController.OnPreviewButtonClick(id);
    }
}
