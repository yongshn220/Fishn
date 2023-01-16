using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public enum CameraType
{
    MainCamera,
    EditFrontCamera,
    EditTopCamera,
}

public class CameraManager : MonoBehaviour
{
    private ViewSceneManager sceneManager;

    private CameraContainer cameraContainer; // Script in the parent Object of all cameras.
    private Camera brainCamera;
    private MainCamera mainCamera;
    private EditFrontCamera editFrontCamera;
    private EditTopCamera editTopCamera;


    public void Setup(ViewSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        this.cameraContainer = sceneManager.fishTankManager.GetCameraContainer();
        SetupCameras();
    }

    void SetupCameras()
    {
        brainCamera = cameraContainer.GetComponentInChildren<CinemachineBrain>()?.GetComponent<Camera>();
        mainCamera = cameraContainer.GetComponentInChildren<MainCamera>();
        editFrontCamera = cameraContainer.GetComponentInChildren<EditFrontCamera>();
        editTopCamera = cameraContainer.GetComponentInChildren<EditTopCamera>();
    }

    public void ChangeCameraView(CameraType type)
    {
        SetAllCameraDeactivate();
        switch (type)
        {
            case CameraType.MainCamera:
                mainCamera.gameObject.SetActive(true);
                break;
            case CameraType.EditFrontCamera:
                editFrontCamera.gameObject.SetActive(true);
                break;
            case CameraType.EditTopCamera:
                editTopCamera.gameObject.SetActive(true);
                break;
            default:
                return;
        }
    }

    private void SetAllCameraDeactivate()
    {
        mainCamera.gameObject.SetActive(false);
        editFrontCamera.gameObject.SetActive(false);
        editTopCamera.gameObject.SetActive(false);
    }

    public Camera GetBrainCamera()
    {
        return brainCamera;
    }
}
