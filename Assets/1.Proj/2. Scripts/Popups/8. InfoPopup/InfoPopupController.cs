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
    private TMP_Text nameText;
    private TMP_Text ageText;
    private TMP_Text feedText;
    private Transform selectPointTr;
    private bool isSetup = false;

    private GameObject selectedEntity;

    void Awake()
    {
        earnButton = transform.GetComponentInChildren<InfoEarnButton>()?.GetComponent<Button>();
        nameText = transform.GetComponentInChildren<InfoNameText>()?.GetComponent<TMP_Text>();
        ageText = transform.GetComponentInChildren<InfoAgeText>()?.GetComponent<TMP_Text>();
        feedText = transform.GetComponentInChildren<InfoFeedText>()?.GetComponent<TMP_Text>();
        selectPointTr = transform.GetComponentInChildren<InfoSelectPoint>()?.GetComponent<Transform>();
    }

    void Start()
    {
        earnButton.onClick.AddListener(OnEarnButtonClick);
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

    public void Enable(){}

    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        this.camera = GameManager.instance.viewSceneManager?.cameraManager.GetBrainCamera();
        this.isSetup = true;
    }
#endregion

#region Button Event
    private void OnEarnButtonClick()
    {
        print("earn button clicked");
    }
#endregion

    private void UpdateSelectPointPosition()
    {
        // set SelectPoint points the selected Entity.
        Vector3 worldPosition = selectedEntity.transform.position + (Vector3.up * selectedEntity.GetComponent<CapsuleCollider>().radius);
        selectPointTr.position = camera.WorldToScreenPoint(worldPosition);
    }

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
        GameObject hitEntity = RaycastHelper.RaycastTagAtMousePosition(camera, "Entity");

        // Object selected -> Save selected Object
        if (hitEntity)
        {
            selectedEntity = hitEntity;
            OpenSelectedEntityInfo();
        }
    }

    private void TryClosePopup()
    {
        GameObject hitEntity = RaycastHelper.RaycastTagAtMousePosition(camera, "Entity");
        List<RaycastResult> results = RaycastHelper.UIRaycastAtMousePoision();

        // Outside of UI clicked -> Close Popup
        if (hitEntity == null && results.Count == 0)
        {
            CloseSelectedEntityInfo();
        }
    }

    private void OpenSelectedEntityInfo()
    { 
        if (!selectedEntity) return;
        
        // Info popup setting.
        EntityMono mono = selectedEntity.GetComponent<EntityMono>();
        var entitySO = GameManager.instance.scriptableObjectManager.TryGetEntitySOById(mono.type_id);
        nameText.text = entitySO.name;
        ageText.text = $"{mono.born_datetime.GetDayPassedFromNow()}";
        feedText.text = $"{mono.feed_datetime.GetHourPassedFromNow()} hrs ago.";

        // Pointing activate
        selectPointTr.gameObject.SetActive(true);

        popupManager.OpenPopup(PopupType.InfoPopup);
    }

    private void CloseSelectedEntityInfo()
    {
        print("Close");
        selectPointTr.gameObject.SetActive(false);
        popupManager.ClosePopup(PopupType.InfoPopup);
    }
}
