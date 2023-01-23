using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BagItemController : MonoBehaviour
{
    BagPopupController popupController;
    public int id;
    public TMP_Text nameText;
    public TMP_Text amountText;
    public Button useButton;

    void Start()
    {
        useButton.onClick.AddListener(OnUseButtonClick);
    }

    public void Setup(BagPopupController popupController, EntityScriptableObjectStructure entitySO, int amount)
    {
        this.popupController = popupController;
        this.id = entitySO.id;
        this.nameText.text = entitySO.name;
    }

    public void Setup(BagPopupController popupController, SeaObjectScriptableObjectStructure seaObjectSO, int amount)
    {
        this.popupController = popupController;
        this.id = seaObjectSO.id;
        this.nameText.text = seaObjectSO.name;
    }

    private void OnUseButtonClick()
    {
        popupController.OnBuyButtonClick(id);
    }
}
