using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public enum EndPointType { LeftMost, RightMost, TopMost, BottomMost, FrontMost, BackMost, }

public class FishManager : MonoBehaviour
{
#region SETUP FISH 
    [Header("Spawn Setup")]

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
    public float[] endPoints;

    public List<GameObject> entityList = new List<GameObject>();

    // Start here. Generate fish depends on the data.
    public void Setup(ViewSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        SetupMovePoint();
        GenerateEntities(GameManager.instance.dataManager.entityDataList);
    }

#region GenerateMovePoint
    // Get Move Points from its children.
    void SetupMovePoint()
    {
        movePoints = transform.GetComponentsInChildren<Transform>()?.Where(t => t.tag == "MovePoint").ToList();
        Vector3 firstVec = movePoints[0].position;
        endPoints = new float[6]{firstVec.x, firstVec.x, firstVec.y, firstVec.y, firstVec.z, firstVec.z};

        foreach (var tr in movePoints)
        {
            if (tr.position.x < endPoints[(int)EndPointType.LeftMost])  endPoints[(int)EndPointType.LeftMost] = tr.position.x;
            if (tr.position.x > endPoints[(int)EndPointType.RightMost])  endPoints[(int)EndPointType.RightMost] = tr.position.x;
            if (tr.position.y < endPoints[(int)EndPointType.TopMost])  endPoints[(int)EndPointType.TopMost] = tr.position.y;
            if (tr.position.y > endPoints[(int)EndPointType.BottomMost])  endPoints[(int)EndPointType.BottomMost] = tr.position.y;
            if (tr.position.z < endPoints[(int)EndPointType.FrontMost])  endPoints[(int)EndPointType.FrontMost] = tr.position.z;
            if (tr.position.z > endPoints[(int)EndPointType.BackMost])  endPoints[(int)EndPointType.BackMost] = tr.position.z;
        }
    }
#endregion

#region Remove
    public void RemoveEntity(int id)
    {
        GameObject entity = entityList.Find((entity) => entity.GetComponent<EntityMono>().id == id);

        if (entity)
        {
            entityList.Remove(entity);
            Destroy(entity);
        }
    }
#endregion

#region GenerateFish
    // Instantiate All fish into the Fish-tank.
    public void GenerateEntity(EntityData entityData)
    {
        if (entityData == null) return;

        GameObject entity = InstantiateFish(entityData);
        SetupEntityMono(entity, entityData);
        SetupEntityAnimator(entity);
        SetupFishController(entity, entityData); // todo : remove.
        SetupFishMovement(entity);
        entityList.Add(entity);
    }

    // Instantiate All fish into the Fish-tank.
    private void GenerateEntities(List<EntityData> entityDataList)
    {
        if (entityDataList.Count < 0) return;

        foreach (EntityData entityData in entityDataList)
        {
            GenerateEntity(entityData);
        }
    }

    private GameObject InstantiateFish(EntityData entityData)
    {
        Vector3 randomMovePointPosition = movePoints[UnityEngine.Random.Range(0, movePoints.Count())].position;;
        Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

        GameObject entityPrefab = GameManager.instance.scriptableObjectManager.TryGetEntityPrefabById(entityData.type_id);
        if (entityPrefab)
        {
            return Instantiate(entityPrefab, randomMovePointPosition, rotation);
        }
        return null;
    }

    private void SetupEntityAnimator(GameObject entity)
    {
        EntityAnimatorController animator = entity.AddComponent<EntityAnimatorController>();
        animator.Setup();
    }

    private void SetupEntityMono(GameObject entity, EntityData entityData)
    {
        EntityMono mono = entity.AddComponent<EntityMono>();
        mono.Setup(entityData);
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

#region Get
    public int GetNumOfCurrentEntityList()
    {
        return entityList.Count();
    }
#endregion
}