using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BagItemController : MonoBehaviour
{
    private BagPopupController popupController;
    private int type_id;
    private ItemType itemType;
    public Image itemImage;
    public TMP_Text nameText;
    public TMP_Text amountText;
    public Button useButton;

    void Start()
    {
        useButton.onClick.AddListener(OnUseButtonClick);
    }

    public void Setup(BagPopupController popupController, SeaObjectScriptableObjectStructure seaObjectSO, int amount)
    {
        this.popupController = popupController;
        this.type_id = seaObjectSO.id;
        this.itemType = seaObjectSO.type;
        this.nameText.text = seaObjectSO.name;
        this.amountText.text = amount.ToString();
        this.itemImage.sprite = seaObjectSO.sprite;
    }

    public void Setup(BagPopupController popupController, CoralScriptableObjectStructure coralPlantSO, int amount)
    {
        this.popupController = popupController;
        this.type_id = coralPlantSO.id;
        this.itemType = coralPlantSO.type;
        this.nameText.text = coralPlantSO.name;
        this.amountText.text = amount.ToString();
        this.itemImage.sprite = coralPlantSO.sprite;
    }

    private void OnUseButtonClick()
    {
        popupController.OnUseButtonClick(type_id, itemType);
    }
}
