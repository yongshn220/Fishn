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

    public new Camera camera;

    private Button blockingButton;
    private Button frontViewButton;
    private Button topViewButton;

    private GameObject selectedSeaObject;

    public GraphicRaycaster raycaster;
    void Awake()    
    {
        blockingButton = GetComponentInChildren<BlockingPanel>()?.GetComponent<Button>();
        frontViewButton = GetComponentInChildren<FrontViewButton>()?.GetComponent<Button>();
        topViewButton = GetComponentInChildren<TopViewButton>()?.GetComponent<Button>();
    }

    void Start()
    {
        blockingButton?.onClick.AddListener(OnBlockingPanelClick);
        frontViewButton?.onClick.AddListener(OnFrontViewButtonClick);
        topViewButton?.onClick.AddListener(OnTopViewButtonClick);
    }

    void Update()
    { 
        // if (currentMode != EditMode.None && Input.GetMouseButtonUp(0)) 
        // {
        //     Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //     if (!Physics.Raycast(ray, out RaycastHit hit))
        //     {
        //         print("a");
        //         // OnBlockingPanelClick();
        //     }
        // }

        //TO DO :: ADD below UI raycast with above physics raycast.
        if (currentMode != EditMode.None  && Input.GetMouseButtonDown(0))
        {
            // Vector2 mousePos = Input.mousePosition;
            // Ray ray = camera.ScreenPointToRay(mousePos);
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            if (results.Count == 0)
            {
                Debug.Log("no UI object: ");
            }
            else
            {
                Debug.Log("Hit UI object: " + results[0].gameObject.name);
            }
        }

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
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("SeaObject"))
                {
                    print(hit.collider.gameObject.name);
                    selectedSeaObject =  hit.collider.gameObject;
                }
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


#region Button Event
    // Outside of the current UI is clicked -> Close the current UI.
    private void OnBlockingPanelClick()
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
