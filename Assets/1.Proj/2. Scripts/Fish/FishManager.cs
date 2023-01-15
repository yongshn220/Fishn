using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishManager : MonoBehaviour
{
#region SETUP FISH 
    [Header("Spawn Setup")]
    [SerializeField] private Vector3 spawnBounds;

    [Range(0.1f, 5)]
    [SerializeField] private float _minSmoothDampTime;
    public float minSmoothDampTime { get { return _minSmoothDampTime; }}

    [Range(0.1f, 5)]
    [SerializeField] private float _maxSmoothDampTime;
    public float maxSmoothDampTime { get { return _maxSmoothDampTime; }}

    [Range(0, 10)]
    [SerializeField] private float _smoothDampLoopTime;
    public float smoothDampLoopTime { get {return _smoothDampLoopTime; }}

    [Header("Speed Setup")]
    [Range(0, 2)]
    [SerializeField] private float _minSpeed;
    public float minSpeed { get { return _minSpeed; }}

    [Range(0, 2)] 
    [SerializeField] private float _maxSpeed; 
    public float maxSpeed { get { return _maxSpeed; }}

    [Range(5, 10)]
    [SerializeField] private float _speedLoopTime;
    public float speedLoopTime { get { return _speedLoopTime; }}

    [Header("Move Point Setup")]
    [Range(1, 10)]
    [SerializeField] private int _minMovePointTime;
    public int minMovePointTime { get { return _minMovePointTime; }}

    [Range(0, 10)]
    [SerializeField] private int _maxMovePointTime;
    public int maxMovePointTime { get { return _maxMovePointTime; }}

    [Range(0, 10)] public int selectedMovePointNum;
    [Range(0, 10)] public int minMovePointTargetTime;
    [Range(0, 10)] public int maxMovePointTargetTime;

    public GameObject testObject;

#endregion

    private ViewSceneManager sceneManager;

    public List<Transform> movePoints;
    public List<GameObject> entityList = new List<GameObject>();

    // Start here. Generate fish depends on the data.
    public void Setup(ViewSceneManager sceneManager, List<FishData> fishDataList)
    {
        this.sceneManager = sceneManager;
        SetupMovePoint();
        GenerateUnits(fishDataList);
        // GenerateUnits(fishDataList);
    }

#region GenerateMovePoint

    // Get Move Points from its children.
    void SetupMovePoint()
    {
        movePoints = transform.GetComponentsInChildren<Transform>()?.Where(t => t.tag == "MovePoint").ToList();
    }
#endregion

#region GenerateFish
    // Instantiate All fish into the Fish-tank.
    void GenerateUnits(List<FishData> fishDataList)
    {
        for (int i = 0; i < fishDataList.Count; i++)
        {
            GameObject EntityObject = InstantiateFish(fishDataList[i]);
            SetupFishController(EntityObject, fishDataList[i]);
            SetupFishMovement(EntityObject);
            entityList.Add(EntityObject);
        }
    }

    GameObject InstantiateFish(FishData fishData)
    {
        Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
        randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
        Vector3 spawnPosition = transform.position + randomVector;
        Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);


        GameObject entityPrefab = GameManager.instance.scriptableObjectManager.TryGetEntityPrefabById(fishData.type_id);
        if (entityPrefab)
        {
            return Instantiate(entityPrefab, spawnPosition, rotation);
        }
        return null;
    }

    void SetupFishController(GameObject entity, FishData fishData)
    {
        if (fishData != null)
        {
            FishController entityCtrl = entity.AddComponent<FishController>();
            entityCtrl.Setup(fishData.id, fishData.type_id, fishData.born_datetime, fishData.feed_datetime);
        }
    }

    void SetupFishMovement(GameObject entity)
    {
        entity.AddComponent<FishMovement>();
        entity.GetComponent<FishMovement>()?.AssignManager(this);
        entity.GetComponent<FishMovement>()?.InitializeSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
    }
#endregion
}