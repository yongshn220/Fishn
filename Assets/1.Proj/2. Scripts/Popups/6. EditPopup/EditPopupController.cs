using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum EditMode
{
    None,
    Front,
    Top,
}

public class EditPopupController : MonoBehaviour, IPopup
{
    private PopupManager popupManager;

    private bool isSetup = false;
    private EditMode currentMode = EditMode.None;
    private PopupType type = PopupType.EditPopup;

    private new Camera camera;
    private Button frontViewButton;
    private Button topViewButton;
    private Button blockingButton;
    private Button removeButton;

    private GameObject selectedSeaObject;
    private GameObject removeTargetSeaObject;
    private int previousTargetLayer;

    void Awake()    
    {
        frontViewButton = GetComponentInChildren<FrontViewButton>()?.GetComponent<Button>();
        topViewButton = GetComponentInChildren<TopViewButton>()?.GetComponent<Button>();
        blockingButton = GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
        removeButton = GetComponentInChildren<RemoveButton>()?.GetComponent<Button>();
    }

    void Start()
    {
        frontViewButton?.onClick.AddListener(OnFrontViewButtonClick);
        topViewButton?.onClick.AddListener(OnTopViewButtonClick);
        blockingButton?.onClick.AddListener(OnBlockingButtonClick);
        removeButton?.onClick.AddListener(OnRemoveButtonClick);
    }

    void Update()
    { 
        if (isSetup && popupManager.currentType == PopupType.EditPopup)
        {
            HandleMouseClickEvent();
            TryMoveObject();
        }
    }

#region Setup
    // IPopup function
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
        this.camera = GameManager.instance.viewSceneManager?.cameraManager.GetBrainCamera();
        this.isSetup = true;
        ResetRemoveButton();
    }

    public void Enable()
    {
        currentMode = EditMode.Front;
        popupManager.SetCoralLightingState(false);
    }

    public void Disable()
    {
        currentMode = EditMode.None;
        popupManager.SetCoralLightingState(true);
    }
#endregion

#region Mouse Click Event
    private void HandleMouseClickEvent()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TryToggleRemoveButton();
        }

        if (Input.GetMouseButtonDown(0))
        {
            TrySelectSeaObject();
            TryClosePopup();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedSeaObject = null; 
        }
    }

    // Clear previous removeButton history.
    private void ResetRemoveButton()
    {
        if (removeTargetSeaObject)
        {
            removeTargetSeaObject.layer = previousTargetLayer;
        }
        removeTargetSeaObject = null;
        removeButton.gameObject.SetActive(false);     // disable remove button.
        blockingButton.gameObject.SetActive(false);
    }

    private void TryToggleRemoveButton()
    {
        GameObject hitObject = RaycastHelper.RaycastAtMousePosition(camera);

        if (hitObject && (hitObject.CompareTag("SeaObject") || hitObject.CompareTag("CoralPlant")))
        {
            ResetRemoveButton(); // Clear previous removeButton history.

            removeTargetSeaObject = hitObject; // save remove-target.
            previousTargetLayer = removeTargetSeaObject.layer; // save current layer #.
            removeTargetSeaObject.layer = (int) LayerType.ObjectLighter; // set new layer #.
            RectTransform canvasRectTransform = popupManager.GetRectTransform();
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null, out localPos);
            removeButton.transform.localPosition = localPos;

            removeButton.gameObject.SetActive(true);
            blockingButton.gameObject.SetActive(true);
        }
    }

    private void TrySelectSeaObject()
    {
        GameObject hitObject = RaycastHelper.RaycastAtMousePosition(camera);

        // Object selected -> Save selected Object
        if (hitObject && (hitObject.CompareTag("SeaObject") || hitObject.CompareTag("CoralPlant")))
        {
            selectedSeaObject = hitObject;
        }
    }

    private void TryClosePopup()
    {
        GameObject hitObject = RaycastHelper.RaycastAtMousePosition(camera);
        List<RaycastResult> results = RaycastHelper.UIRaycastAtMousePoision();

        // Outside of UI clicked -> Close Popup
        if (hitObject == null && results.Count == 0)
        {
            UpdateAllSeaObjectPosition();
            ClosePopup();
        }
    }
#endregion

#region Edit | Save
    private void TryMoveObject()
    {
        if (selectedSeaObject)
        {
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");
            if (currentMode == EditMode.Front)
            {
                selectedSeaObject.transform.Translate(new Vector3(horizontal, vertical, 0)); return;
            }
            if (currentMode == EditMode.Top)
            {
                selectedSeaObject.transform.Translate(new Vector3(horizontal, 0, vertical)); return;
            }
        }
    }

    private void UpdateAllSeaObjectPosition()
    {
        popupManager.UpdateAllSeaObjectPosition();
    }
#endregion

#region Button Event
    // Outside of the current UI is clicked -> Close the current UI.
    private void ClosePopup()
    {
        ResetRemoveButton();
        popupManager.ChangeCameraView(CameraType.MainCamera);
        popupManager.ClosePopup(this.type);
    }

    private void OnFrontViewButtonClick()
    {
        currentMode = EditMode.Front;
        popupManager.ChangeCameraView(CameraType.EditFrontCamera);
    }

    private void OnTopViewButtonClick()
    {
        currentMode = EditMode.Top;
        popupManager.ChangeCameraView(CameraType.EditTopCamera);
    }

    private void OnBlockingButtonClick()
    {
        ResetRemoveButton();
    }

    private void OnRemoveButtonClick()
    {
        if (removeTargetSeaObject)
        {
            SeaObjectMono seaObjectMono;
            if (removeTargetSeaObject.TryGetComponent<SeaObjectMono>(out seaObjectMono))
            {
                popupManager.RemoveSeaObjectFromTank(seaObjectMono);
            }
            CoralPlantMono coralPlantMono;
            if (removeTargetSeaObject.TryGetComponent<CoralPlantMono>(out coralPlantMono))
            {
                popupManager.RemoveCoralPlantFromTank(coralPlantMono);
            }
        }
        ResetRemoveButton();
    }
#endregion
}
