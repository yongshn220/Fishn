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
        SeaObjectSetup(GameManager.instance.dataManager.seaObjectDataList);
    }

 #region Load | Instantiate 
    private void SeaObjectSetup(List<SeaObjectData> seaObjectDataList)
    {
        foreach (var data in seaObjectDataList)
        {
            InstantiateSeaObject(data);
        }
    }

    public void InstantiateSeaObject(SeaObjectData data)
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

#region Save
    public void SaveSeaObjectData()
    {
        List<SeaObjectData> allSeaObjectData = GetExistSeaObjectDataList();
        GameManager.instance.dataManager.SaveSeaObjectData(allSeaObjectData);
    }
#endregion

#region Update

    public void UpdateAllSeaObjectMonoPosition()
    {
        foreach (SeaObjectMono data in enabledSeaObjectMonoList)
        {
            data.position = data.transform.position;
        }
        SaveSeaObjectData();
    }

    public void RemoveSeaObjectFromTank(SeaObjectMono targetMono)
    {
        targetMono.instantiated = false;
        enabledSeaObjectMonoList.Remove(targetMono);
        disabledSeaObjectDataList.Add(targetMono.ToData());
        Destroy(targetMono);
        //Callback. to do
        SaveSeaObjectData();
    }

    public void LoadSeaObjectFromBag(SeaObjectData targetData)
    {
        SaveSeaObjectData();
    }
#endregion

    private void RemoveSeaObject()
    {

    }

    // Gather the enabled Mono and disabled data into one List<SeaObjectData> and return.
    private List<SeaObjectData> GetExistSeaObjectDataList()
    {
        List<SeaObjectData> resultList = new List<SeaObjectData>();

        resultList.AddRange(enabledSeaObjectMonoList.ConvertToData());
        resultList.AddRange(disabledSeaObjectDataList);

        return resultList;
    }

    public CameraContainer GetCameraContainer()
    {
        return cameraContainer;
    }
}
