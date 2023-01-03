using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{   
    [Header("Spawn Setup")]
    [SerializeField] private FishHandler flockUnitPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;
    
    [Header("Speed Setup")]
    [Range(0, 10)]
    [SerializeField] private float _minSpeed;
    public float minSpeed { get { return _minSpeed; } }
    [Range(0, 10)]
    [SerializeField] private float _maxSpeed;
    public float maxSpeed { get { return _maxSpeed; } }


    [Header("Detection Distances")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionDistance;
    public float cohesionDistance { get { return _cohesionDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidanceDistance;
    public float avoidanceDistance { get { return _avoidanceDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _alignmentDistance;
    public float alignmentDistance { get { return _alignmentDistance; } }
    
    [Range(0, 10)]
    [SerializeField] private float _boundsDistance;
    public float boundsDistance { get { return _boundsDistance; } }
    
    [Range(0, 10)]
    [SerializeField] private float _obstacleDistance;
    public float obstacleDistance { get { return _obstacleDistance; } }

    [Header("Behaviour Weights")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionWeight;
    public float cohesionWeight { get { return _cohesionWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidanceWeight;
    public float avoidanceWeight { get { return _avoidanceWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _alignmentWeight;
    public float alignmentWeight { get { return _alignmentWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _boundsWeight;
    public float boundsWeight { get { return _boundsWeight; } }
    
    [Range(0, 10)]
    [SerializeField] private float _obstacleWeight;
    public float obstacleWeight { get { return _obstacleDistance; } }
    
    public FishHandler[] allUnits { get; set; }

    private void Start()
    {
        GenerateUnits();
    }  

    private void Update() 
    {
        for (int i = 0; i < allUnits.Length; i++)
        {
            allUnits[i].MoveUnit();
        }
    }

    private void GenerateUnits()
    {
        allUnits = new FishHandler[flockSize];
        for (int i = 0; i < flockSize; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitializedSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
        }
    }
}
