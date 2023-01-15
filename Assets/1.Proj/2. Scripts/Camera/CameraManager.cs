using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CameraType
{
    MainCamera,
    EditFrontCamera,
    EditTopCamera,
}
public class CameraManager : MonoBehaviour
{
    private MainCamera mainCamera;
    private EditFrontCamera editFrontCamera;
    private EditTopCamera editTopCamera;

    void Awake()
    {
        mainCamera = GetComponentInChildren<MainCamera>();
        editFrontCamera = GetComponentInChildren<EditFrontCamera>();
        editTopCamera = GetComponentInChildren<EditTopCamera>();
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
}
