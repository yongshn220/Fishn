using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FishTankController : MonoBehaviour
{
    [SerializeField]
    private int id;

    private CameraManager cameraManager;
    private Transform previewTransform;
    private Transform structureTransform;
    
    void Awake()
    {
        cameraManager = GetComponentInChildren<CameraManager>();
        previewTransform = GetComponentInChildren<FishTankPreview>()?.GetComponent<Transform>();
        structureTransform = GetComponentInChildren<FishTankStructure>()?.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    public void Setup()
    {
        List<SeaPlantData> seaPlantList = new List<SeaPlantData>();
        seaPlantList.Add(new SeaPlantData(1, 1, new Vector3(0, 0, 0)));
        seaPlantList.Add(new SeaPlantData(2, 2, new Vector3(1, 0, 1)));

        LoadStructures(seaPlantList);
    }

    private void LoadStructures(List<SeaPlantData> seaPlantList)
    {
        GameObject prefab;
        foreach (var plant in seaPlantList)
        {
            prefab = GameManager.instance.scriptableObjectManager.TryGetSeaPlantPrefabById(plant.id);
            if (prefab)
            {
                Instantiate(prefab, plant.position, Quaternion.identity, structureTransform);
            }
        }
    }

    public void ChangeCameraView(CameraType type)
    {
        cameraManager.ChangeCameraView(type);
    }
}
