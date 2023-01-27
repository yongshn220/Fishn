using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InfoPopupController : MonoBehaviour, IPopup
{
    private PopupManager popupManager;
    private new Camera camera;
    private Button sellButton;
    private TMP_Text nameText;
    private TMP_Text ageText;
    private TMP_Text feedText;
    private bool isSetup = false;

    private GameObject selectedEntity;

    void Awake()
    {
        sellButton = transform.GetComponentInChildren<SellButton>()?.GetComponent<Button>();
        nameText = transform.GetComponentInChildren<InfoNameText>()?.GetComponent<TMP_Text>();
        ageText = transform.GetComponentInChildren<InfoAgeText>()?.GetComponent<TMP_Text>();
        feedText = transform.GetComponentInChildren<InfoFeedText>()?.GetComponent<TMP_Text>();
    }

    void Start()
    {
        sellButton.onClick.AddListener(OnSellButtonClick);
    }

    void Update()
    {
        if (isSetup && popupManager.currentType == PopupType.MainUIPopup )
        {
            HandleMouseClickEvent();
        }
    }


#region IPopup
    public void Disable(){}

    public void Enable(){}

    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        this.camera = GameManager.instance.viewSceneManager?.cameraManager.GetBrainCamera();
        this.isSetup = true;
    }
#endregion

#region Button Event
    private void OnSellButtonClick()
    {
        print("sell button clicked");
    }
#endregion

    private void HandleMouseClickEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectSeaObject();
            TryClosePopup();
        }
    }
    // Raycast
    private void TrySelectSeaObject()
    {
        GameObject hitObject = RaycastHelper.RaycastAtMousePosition(camera);

        // Object selected -> Save selected Object
        if (hitObject && hitObject.CompareTag("Entity"))
        {
            selectedEntity = hitObject;
            OpenSelectedEntityInfo();
        }
    }

    private void TryClosePopup()
    {
        GameObject hitObject = RaycastHelper.RaycastAtMousePosition(camera);
        List<RaycastResult> results = RaycastHelper.UIRaycastAtMousePoision();

        // Outside of UI clicked -> Close Popup
        if (hitObject == null && results.Count == 0)
        {
            CloseSelectedEntityInfo();
        }
    }

    private void OpenSelectedEntityInfo()
    { 
        if (!selectedEntity) return;
        
        EntityMono mono = selectedEntity.GetComponent<EntityMono>();
        var entitySO = GameManager.instance.scriptableObjectManager.TryGetEntitySOById(mono.type_id);
        nameText.text = entitySO.name;
        ageText.text = $"{mono.born_datetime.GetDayPassedFromNow()}";
        feedText.text = $"{mono.feed_datetime.GetHourPassedFromNow()} hrs ago.";

        popupManager.OpenPopup(PopupType.InfoPopup);
    }

    private void CloseSelectedEntityInfo()
    {
        popupManager.ClosePopup(PopupType.InfoPopup);
    }
}
