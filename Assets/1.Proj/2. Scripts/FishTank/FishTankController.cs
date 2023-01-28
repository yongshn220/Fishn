using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;



public class FishTankController : MonoBehaviour
{
    [SerializeField]
    private int id;

    private CameraContainer cameraContainer;
    private Transform previewTransform;
    private Transform structureTransform;

    // Sea Object Lists
    private List<SeaObjectMono> enabledSeaObjectMonoList = new List<SeaObjectMono>(); // Instantiated
    private List<SeaObjectData> disabledSeaObjectDataList = new List<SeaObjectData>(); // Uninstantiated 
    public List<SeaObjectData> disabledSeaObjectDataListDeepCopy { get {return disabledSeaObjectDataList.DeepCopy();}}

    // Coral Plant Lists
    private List<CoralPlantMono> enabledCoralPlantMonoList = new List<CoralPlantMono>(); // Instantiated
    private List<CoralPlantData> disabledCoralPlantDataList = new List<CoralPlantData>(); // Uninstantiated 
    public List<CoralPlantData> disabledCoralPlantDataListDeepCopy { get {return disabledCoralPlantDataList.DeepCopy();}}
    
    public static event Action OnSeaObjectUpdate;
    
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
        CoralPlantSetup(GameManager.instance.dataManager.coralPlantDataList);
    }

 #region Load | Instantiate 
    private void SeaObjectSetup(List<SeaObjectData> seaObjectDataList)
    {
        foreach (var data in seaObjectDataList)
        {
            InstantiateSeaObject(data);
        }
    }

    private void CoralPlantSetup(List<CoralPlantData> coralPlantDataList)
    {
        foreach (var data in coralPlantDataList)
        {
            InstantiateCoralPlant(data);
        }
    }

    public void InstantiateSeaObject(SeaObjectData data)
    {
        GameObject prefab = GameManager.instance.scriptableObjectManager.TryGetSeaObjectPrefabById(data.type_id);

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

    public void InstantiateCoralPlant(CoralPlantData data)
    {
        var coralPlantSO = GameManager.instance.scriptableObjectManager.TryGetCoralPlantSOById(data.type_id);

        if (coralPlantSO == null) { Debug.LogError("No following prefab exists."); return; }
        
        if (data.instantiated)
        {
            GameObject seaObject = Instantiate(coralPlantSO.prefab, data.position, Quaternion.identity, structureTransform);
            CoralPlantMono coralPlantMono = seaObject.AddComponent<CoralPlantMono>();
            coralPlantMono.Setup(data, coralPlantSO);
            coralPlantMono.instantiated = true;
            enabledCoralPlantMonoList.Add(coralPlantMono);
        }
        else
        {
            disabledCoralPlantDataList.Add(data);
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
        Destroy(targetMono.gameObject);
        OnSeaObjectUpdate?.Invoke(); // Delegate Invoke.
        SaveSeaObjectData();
    }

    public void LoadSeaObjectFromBag(int type_id)
    {
        foreach (var data in disabledSeaObjectDataList)
        {
            if (data.type_id == type_id)
            {
                data.instantiated = true;
                data.position = Vector3.zero;
                InstantiateSeaObject(data);
                disabledSeaObjectDataList.Remove(data);
                break;
            }
        }
        OnSeaObjectUpdate?.Invoke(); // Delegate Invoke.
        SaveSeaObjectData();
    }
#endregion

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
