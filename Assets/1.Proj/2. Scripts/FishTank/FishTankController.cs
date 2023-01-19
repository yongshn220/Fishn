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
        LoadSeaObjects(GameManager.instance.dataManager.seaObjectDataList);
    }

    private void LoadSeaObjects(List<SeaObjectData> seaObjectDataList)
    {
        GameObject prefab;
        print(seaObjectDataList);
        
        foreach (var data in seaObjectDataList)
        {
            prefab = GameManager.instance.scriptableObjectManager.TryGetSeaPlantPrefabById (data.id);
            if (prefab)
            {
                GameObject seaObject = Instantiate(prefab, data.position, Quaternion.identity, structureTransform);
                SeaObjectData instantiatedSeaObjectData = seaObject.AddComponent<SeaObjectData>();
                instantiatedSeaObjectData.Setup(data);
                instantiatedSeaObjectData.instantiated = true;
                curSeaObjectDataList.Add(instantiatedSeaObjectData);
            }
        }
    }

    public void SaveSeaObjectData()
    {
        UpdateSeaObjectPosition();
        GameManager.instance.dataManager.SaveSeaObjectData(curSeaObjectDataList);
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
