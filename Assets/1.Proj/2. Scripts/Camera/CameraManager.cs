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

    public void ChangeCamera(CameraType type)
    {
        
    }
}
