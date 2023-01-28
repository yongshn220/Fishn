using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BagItemController : MonoBehaviour
{
    private BagPopupController popupController;
    private int type_id;

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
        this.type_id = entitySO.id;
        this.nameText.text = entitySO.name;
        this.amountText.text = amount.ToString();
    }

    public void Setup(BagPopupController popupController, SeaObjectScriptableObjectStructure seaObjectSO, int amount)
    {
        this.popupController = popupController;
        this.type_id = seaObjectSO.id;
        this.nameText.text = seaObjectSO.name;
        this.amountText.text = amount.ToString();
    }

    private void OnUseButtonClick()
    {
        popupController.OnUseButtonClick(type_id);
    }
}
