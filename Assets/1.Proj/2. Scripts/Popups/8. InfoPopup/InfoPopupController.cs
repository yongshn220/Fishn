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
    private Button earnButton;
    private Button sellButton;
    private TMP_Text nameText;
    private TMP_Text ageText;
    private TMP_Text feedText;
    private Transform selectPointTr;
    private bool isSetup = false;

    private EntityMono selectedEntity;

    void Awake()
    {
        earnButton = transform.GetComponentInChildren<InfoEarnButton>()?.GetComponent<Button>();
        sellButton = transform.GetComponentInChildren<InfoSellButton>()?.GetComponent<Button>();
        nameText = transform.GetComponentInChildren<InfoNameText>()?.GetComponent<TMP_Text>();
        ageText = transform.GetComponentInChildren<InfoAgeText>()?.GetComponent<TMP_Text>();
        feedText = transform.GetComponentInChildren<InfoFeedText>()?.GetComponent<TMP_Text>();
        selectPointTr = transform.GetComponentInChildren<InfoSelectPoint>()?.GetComponent<Transform>();
    }

    void Start()
    {
        earnButton.onClick.AddListener(OnEarnButtonClick);
        sellButton.onClick.AddListener(OnSellButtonClick);
    }

    void Update()
    {
        if (isSetup && (popupManager.currentType == PopupType.MainUIPopup || popupManager.currentType == PopupType.InfoPopup))
        {
            HandleMouseClickEvent();
        }

        if (selectedEntity)
        {
            UpdateSelectPointPosition();
        }
    }


#region IPopup
    public void Disable(){}

    public void Enable(int option){}

    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        this.camera = GameManager.instance.viewSceneManager?.cameraManager.GetBrainCamera();
        DelegateManager.OnEntityMonoUpdate += OnEntityMonoUpdate;
        this.isSetup = true;
    }
#endregion

#region Update
    private void UpdateSelectPointPosition()
    {
        // set SelectPoint points the selected Entity.
        Vector3 worldPosition = selectedEntity.transform.position + (Vector3.up * selectedEntity.GetComponent<CapsuleCollider>().radius);
        selectPointTr.position = camera.WorldToScreenPoint(worldPosition);
    }
#endregion

#region Delegate Callback
    private void OnEntityMonoUpdate(EntityMono mono)
    {
        if (!selectedEntity) return;
        if (selectedEntity != mono) return;
        ShowMonoData(mono);
    }
#endregion

#region Button Event
    private void OnEarnButtonClick()
    {
        print("earn button clicked");
    }

    private void OnSellButtonClick()
    {
        this.popupManager.TrySellItem(selectedEntity.id, ItemType.Entity, selectedEntity.coral).Forget();
    }
#endregion

#region Mouse Click Event
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
        if (popupManager.currentType != PopupType.MainUIPopup && popupManager.currentType != PopupType.InfoPopup) return;
        print(popupManager.currentType);
        GameObject hitEntity = RaycastHelper.RaycastTagAtMousePosition(camera, "Entity");

        // Object selected -> Save selected Object
        if (hitEntity)
        {
            selectedEntity = hitEntity.GetComponent<EntityMono>();
            OpenSelectedEntityInfo();
        }
    }

    private void TryClosePopup()
    {
        if (popupManager.currentType != PopupType.InfoPopup) return;

        GameObject hitEntity = RaycastHelper.RaycastTagAtMousePosition(camera, "Entity");
        List<RaycastResult> results = RaycastHelper.UIRaycastAtMousePoision();

        // Outside of UI clicked -> Close Popup
        if (hitEntity == null && results.Count == 0)
        {
            print("closed");
            CloseSelectedEntityInfo();
        }
    }
#endregion

    private void OpenSelectedEntityInfo()
    { 
        if (!selectedEntity) return;
        // Info popup setting.
        EntityMono mono = selectedEntity.GetComponent<EntityMono>();
        if (!mono) return;
        ShowMonoData(mono);

        // Pointing activate
        selectPointTr.gameObject.SetActive(true);

        popupManager.OpenPopup(PopupType.InfoPopup);
    }

    private void ShowMonoData(EntityMono mono)
    {
        nameText.text = mono.name;
        ageText.text = $"{mono.born_datetime.GetDayPassedFromNow()}";
        feedText.text = $"{ (float)mono.feed / (float)mono.maxFeed * 100 } %";
    }

    private void CloseSelectedEntityInfo()
    {
        selectedEntity = null;
        selectPointTr.gameObject.SetActive(false);
        popupManager.ClosePopup(PopupType.InfoPopup);
    }
}
