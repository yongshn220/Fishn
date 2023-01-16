using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPopupController : MonoBehaviour, IPopup
{
    private PopupManager popupManager;

    private PopupType type = PopupType.EditPopup;
    public new Camera camera;
    private Button blockingButton;
    private Button frontViewButton;
    private Button topViewButton;

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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("SeaObject"))
                {
                    print(hit.collider.gameObject.name);
                }
                else
                {
                    print("x : " + hit.collider.gameObject.name);
                }
            }
        }
    }
#region IPopup
    public void Setup(PopupManager popupManager)
    {
        this.popupManager = popupManager;
    }
#endregion

    // Outside of the current UI is clicked -> Close the current UI.
    private void OnBlockingPanelClick()
    {
        popupManager.ChangeCameraView(CameraType.MainCamera);
        popupManager.ClosePopup(this.type);
    }

    private void OnFrontViewButtonClick()
    {
        popupManager.ChangeCameraView(CameraType.EditFrontCamera);
    }

    private void OnTopViewButtonClick()
    {
        popupManager.ChangeCameraView(CameraType.EditTopCamera);
    }
}
