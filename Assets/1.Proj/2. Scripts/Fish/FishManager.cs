using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishManager : MonoBehaviour
{
#region SETUP
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
    [Range(0, 10)]
    [SerializeField] private float _minCohesionWeight;
    public float minCohesionWeight { get { return _minCohesionWeight; }}

    [Range(0, 10)]
    [SerializeField] private float _maxCohesionWeight;
    public float maxCohesionWeight { get { return _maxCohesionWeight; }}

    [Range(0, 10)]
    [SerializeField] private float _minAvoidanceWeight;
    public float minAvoidanceWeight { get { return _minAvoidanceWeight; }}

    [Range(0, 10)]
    [SerializeField] private float _maxAvoidanceWeight;
    public float maxAvoidanceWeight { get { return _maxAvoidanceWeight; }}

    [Range(0, 10)]
    [SerializeField] private float _minAvoidanceDistance;
    public float minAvoidanceDistance { get { return _minAvoidanceDistance; }}

    [Range(0, 20)]
    [SerializeField] private float _maxAvoidanceDistance;
    public float maxAvoidanceDistance { get { return _maxAvoidanceDistance; }}

    [Range(1, 10)]
    [SerializeField] private int _minLoopTime;
    public int minLoopTime { get { return _minLoopTime; }}

    [Range(0, 10)]
    [SerializeField] private int _maxLoopTime;
    public int maxLoopTime { get { return _maxLoopTime; }}


    [Header("Transition Setup")]
    [Range(0, 10)]
    [SerializeField] private float _minTargetDistance;

    public float minTargetdistance { get {return _minTargetDistance; }}

    [Header("Test Objects")]
    public GameObject testObject;
#endregion

    public MovePointController[] movePoints;

    public List<GameObject> fishList = new List<GameObject>();

    // Start here. Generate fish depends on the data.
    public void Generate(List<FishData> fishDataList)
    {
        GenerateMovePoints();
        GenerateUnits(fishDataList);
    }


    // Get Move Points from its children.
    void GenerateMovePoints()
    {
        movePoints = transform.GetComponentsInChildren<MovePointController>();
        foreach (var point in movePoints)
        {
            point.Setup(this);
        }
    }

#region GenerateFish
    // Instantiate All fish into the Fish-tank.
    void GenerateUnits(List<FishData> fishDataList)
    {
        for (int i = 0; i < fishDataList.Count; i++)
        {
            GameObject EntityObject = InstantiateFish(fishDataList[i]);
            SetupFishController(EntityObject, fishDataList[i]);
            SetupFishMovement(EntityObject);
            fishList.Add(EntityObject);
        }
    }


    GameObject InstantiateFish(FishData fishData)
    {
        Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
        randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
        Vector3 spawnPosition = transform.position + randomVector;
        Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

        GameObject entityPrefab = GameManager.instance.prefabManager.getEntityPrefabById(fishData.type_id);
        return Instantiate(entityPrefab, spawnPosition, rotation);
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