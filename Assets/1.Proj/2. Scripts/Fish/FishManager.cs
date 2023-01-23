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
    public void Setup(ViewSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        SetupMovePoint();
        GenerateUnits(GameManager.instance.dataManager.entityDataList);
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
    private void GenerateUnits(List<EntityData> entityDataList)
    {
        if (entityDataList.Count < 0) return;

        foreach (EntityData entityData in entityDataList)
        {
            GameObject EntityObject = InstantiateFish(entityData);
            SetupFishController(EntityObject, entityData);
            SetupFishMovement(EntityObject);
            entityList.Add(EntityObject);
        }
    }

    private GameObject InstantiateFish(EntityData entityData)
    {
        Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
        randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
        Vector3 spawnPosition = transform.position + randomVector;
        Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);


        GameObject entityPrefab = GameManager.instance.scriptableObjectManager.TryGetEntityPrefabById(entityData.type_id);
        if (entityPrefab)
        {
            return Instantiate(entityPrefab, spawnPosition, rotation);
        }
        return null;
    }

    private void SetupFishController(GameObject entity, EntityData entityData)
    {
        if (entityData != null)
        {
            FishController entityCtrl = entity.AddComponent<FishController>();
            entityCtrl.Setup(entityData.id, entityData.type_id, entityData.born_datetime, entityData.feed_datetime);
        }
    }

    private void SetupFishMovement(GameObject entity)
    {
        entity.AddComponent<FishMovement>();
        entity.GetComponent<FishMovement>()?.AssignManager(this);
        entity.GetComponent<FishMovement>()?.InitializeSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
    }
#endregion
}