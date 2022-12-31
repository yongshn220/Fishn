using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishManager : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] private FishController fishPrefab;
    [SerializeField] private int fishSize;
    [SerializeField] private Vector3 spawnBounds;

    [Header("Speed Setup")]
    [Range(0, 10)]
    [SerializeField] private float _minSpeed;
    public float minSpeed { get { return _minSpeed; } }

    [Range(0, 10)]
    [SerializeField] private float _maxSpeed; 
    public float maxSpeed { get { return _maxSpeed; } }

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

    public MovePointController[] movePoints;

    public FishController[] allFish {get; set;}

    [Header("Transition Setup")]
    [Range(0, 10)]
    [SerializeField] private float _minTargetDistance;
    public float minTargetdistance { get {return _minTargetDistance; }}

    [Header("Test Objects")]
    public GameObject testObject;

    void Start()
    {
        GenerateMovePoints();
        GenerateUnits();
    }

    void GenerateMovePoints()
    {
        movePoints = transform.GetComponentsInChildren<MovePointController>();
        foreach (var point in movePoints)
        {
            point.Setup(this);
        }
    }

    void GenerateUnits()
    {
        allFish = new FishController[fishSize];
        for (int i = 0; i < fishSize; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            allFish[i] = Instantiate(fishPrefab, spawnPosition, rotation);
            allFish[i].AssignManager(this);
            allFish[i].InitializeSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
        }
    }
}
