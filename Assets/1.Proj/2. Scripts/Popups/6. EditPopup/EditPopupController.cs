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

    private GameObject selectedSeaObject;

    void Awake()    
    {
        frontViewButton = GetComponentInChildren<FrontViewButton>()?.GetComponent<Button>();
        topViewButton = GetComponentInChildren<TopViewButton>()?.GetComponent<Button>();
    }

    void Start()
    {
        frontViewButton?.onClick.AddListener(OnFrontViewButtonClick);
        topViewButton?.onClick.AddListener(OnTopViewButtonClick);
    }

    void Update()
    { 
        if (isSetup && popupManager.currentType == PopupType.EditPopup)
        {
            TrySelectSeaObject();
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
    }

    public void Enable()
    {
        currentMode = EditMode.Front;
    }

    public void Disable()
    {
        currentMode = EditMode.None;
    }
#endregion


    private void TrySelectSeaObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = RaycastHelper.RaycastAtMousePosition(camera);
            List<RaycastResult> results = RaycastHelper.UIRaycastAtMousePoision();

            if (hitObject && hitObject.CompareTag("SeaObject"))
            {
                selectedSeaObject = hitObject;
            }

            // Outside of UI clicked
            if (hitObject == null && results.Count == 0)
            {
                SaveSeaObjetData();
                ClosePopup();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedSeaObject = null;
        }
    }

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

    private void SaveSeaObjetData()
    {
        popupManager.SaveSeaObjetData();
    }

#region Button Event
    // Outside of the current UI is clicked -> Close the current UI.
    private void ClosePopup()
    {
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
#endregion
}
