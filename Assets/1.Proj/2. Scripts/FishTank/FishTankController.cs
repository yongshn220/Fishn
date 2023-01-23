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

    private List<SeaObjectMono> enabledSeaObjectMonoList = new List<SeaObjectMono>(); // Instantiated
    private List<SeaObjectData> disabledSeaObjectDataList = new List<SeaObjectData>(); // Uninstantiated 
    public List<SeaObjectData> disabledSeaObjectDataListDeepCopy { get {return disabledSeaObjectDataList.DeepCopy();}}
    
    
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

 #region Add | Instantiate 
    private void LoadSeaObjects(List<SeaObjectData> seaObjectDataList)
    {
        foreach (var data in seaObjectDataList)
        {
            AddSeaObject(data);
        }
    }

    public void AddSeaObject(SeaObjectData data)
    {
        GameObject prefab = GameManager.instance.scriptableObjectManager.TryGetSeaPlantPrefabById(data.type_id);

        if (!prefab) { Debug.LogError("No following prefab exists."); return; }
        
        if (data.instantiated)
        {
            GameObject seaObject = Instantiate(prefab, data.position, Quaternion.identity, structureTransform);
            SeaObjectMono seaObjectMono = seaObject.AddComponent<SeaObjectMono>();
            seaObjectMono.Setup(data);
            seaObjectMono.instantiated = true;
            enabledSeaObjectMonoList.Add(seaObjectMono);
        }
        else
        {
            disabledSeaObjectDataList.Add(data);
        }
    }
#endregion

#region Update | Save
    public void SaveSeaObjectData()
    {
        UpdateSeaObjectPosition();
        GameManager.instance.dataManager.SaveSeaObjectData(enabledSeaObjectMonoList);
    }

    private void UpdateSeaObjectPosition()
    {
        foreach (SeaObjectMono data in enabledSeaObjectMonoList)
        {
            data.position = data.transform.position;
        }
    }
#endregion

    public CameraContainer GetCameraContainer()
    {
        return cameraContainer;
    }
}
