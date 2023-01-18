using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FishTankController : MonoBehaviour
{
    [SerializeField]
    private int id;

    private CameraContainer cameraContainer;
    private Transform previewTransform;
    private Transform structureTransform;

    private List<SeaObjectData> curSeaObjectDataList = new List<SeaObjectData>();
    
    void Awake()
    {
        cameraContainer = GetComponentInChildren<CameraContainer>();
        previewTransform = GetComponentInChildren<FishTankPreview>()?.GetComponent<Transform>();
        structureTransform = GetComponentInChildren<FishTankStructure>()?.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    public void Setup()
    {
        List<SeaObjectData> seaObjectDataList = new List<SeaObjectData>();

        SeaObjectData newData = new SeaObjectData();
        newData.Setup(1,1, new Vector3(0,0,0));
        seaObjectDataList.Add(newData);

        newData = new SeaObjectData();
        newData.Setup(2,2, new Vector3(1, 0, 1));
        seaObjectDataList.Add(newData);

        LoadSeaObjects(seaObjectDataList);
    }

    private void LoadSeaObjects(List<SeaObjectData> seaObjectDataList)
    {
        GameObject prefab;
        foreach (var data in seaObjectDataList)
        {
            prefab = GameManager.instance.scriptableObjectManager.TryGetSeaPlantPrefabById (data.id);
            if (prefab)
            {
                GameObject seaObject = Instantiate(prefab, data.position, Quaternion.identity, structureTransform);
                SeaObjectData seaObjectData = seaObject.AddComponent<SeaObjectData>();
                seaObjectData.Setup(data);
                seaObjectData.isInstantiated = true;
                curSeaObjectDataList.Add(data);
            }
        }
    }

    private void SaveSeaObjects()
    {
        UpdateSeaObjectPosition();
        // GameManager.instance.dataManager.SaveSeaObjectData(curSeaObjectDataList);
    }

    private void UpdateSeaObjectPosition()
    {
        foreach (SeaObjectData data in curSeaObjectDataList)
        {
            data.position = data.transform.position;
        }
    }

    public CameraContainer GetCameraContainer()
    {
        return cameraContainer;
    }
}
