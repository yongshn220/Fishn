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

    private List<SeaObjectMono> curSeaObjectMonoList = new List<SeaObjectMono>();
    
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
            prefab = GameManager.instance.scriptableObjectManager.TryGetSeaPlantPrefabById (data.type_id);
            if (prefab)
            {
                GameObject seaObject = Instantiate(prefab, data.position, Quaternion.identity, structureTransform);
                SeaObjectMono instantiatedSeaObjectData = seaObject.AddComponent<SeaObjectMono>();
                instantiatedSeaObjectData.Setup(data);
                instantiatedSeaObjectData.instantiated = true;
                curSeaObjectMonoList.Add(instantiatedSeaObjectData);
            }
        }
    }

    public void SaveSeaObjectData()
    {
        UpdateSeaObjectPosition();
        GameManager.instance.dataManager.SaveSeaObjectData(curSeaObjectMonoList);
    }

    private void UpdateSeaObjectPosition()
    {
        foreach (SeaObjectMono data in curSeaObjectMonoList)
        {
            data.position = data.transform.position;
        }
    }

    public CameraContainer GetCameraContainer()
    {
        return cameraContainer;
    }
}
