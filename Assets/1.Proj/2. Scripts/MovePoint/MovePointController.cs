using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointController : MonoBehaviour
{
    private FishManager fishManager;
    private float cohesionWeightAbs;
    private float cohesionDistanceAbs;
    private float avoidanceWeightAbs;
    private float avoidanceDistanceAbs;
    private int loopTime;
    private int cohesionWeightDir = 1; // -1 or 1
    private int avoidanceWeightDir = 1; // -1 or 1
    private bool isSetup = false;
    public float currentCohesionWeight;
    public float currentCohesionDistance;
    public float currentAvoidanceWeight;
    public float currentAvoidanceDistance;


    void Start()
    {

    }

    void Update()
    {
        if (!isSetup) return;
        UpdateWeightAndDistance();
        UpdateDirection();
    }

    public void Setup(FishManager fishManager)
    {
        this.fishManager = fishManager;
        InitializeAllValues();
        isSetup = true;
    }

    // Gradually increase or decrease the values depends on the looptime.
    private void UpdateWeightAndDistance()
    {
        currentCohesionWeight = currentCohesionWeight + cohesionWeightDir * (cohesionWeightAbs / loopTime) * Time.deltaTime;
        currentAvoidanceWeight = currentAvoidanceWeight + avoidanceWeightDir * (avoidanceWeightAbs / loopTime) * Time.deltaTime;

        print(currentAvoidanceWeight);
    }

    // Change direction of Update value (increase / decrease).
    private void UpdateDirection()
    {
        if (currentCohesionWeight <= -cohesionWeightAbs)
        {
            cohesionWeightDir = 1;
            SetNewCohesionWeight();
        }
        else if (currentCohesionWeight >= cohesionWeightAbs)
        {
            cohesionWeightDir = -1;
            SetNewCohesionWeight();
        }

        if (currentAvoidanceWeight <= -avoidanceWeightAbs)
        {
            avoidanceWeightDir = 1;
            SetNewAvoidanceWeight();
        }

        else if (currentAvoidanceWeight >= avoidanceWeightAbs)
        {
            avoidanceWeightDir = -1;
            SetNewAvoidanceWeight();
        }
    }

    // Set new max values for the cohesion, avoidance, and looptime depends on the FishManager.
    private void InitializeAllValues()
    {
        SetNewCohesionWeight();
        print("cohW : " + cohesionWeightAbs);
        SetNewAvoidanceWeight();
        print("avodW : " + avoidanceWeightAbs);
        SetNewAvoidanceDistance();
        print("avodD : " + avoidanceDistanceAbs);
        SetNewLoopTime();
        currentCohesionWeight = UnityEngine.Random.Range(0, cohesionWeightAbs);
        currentAvoidanceWeight = UnityEngine.Random.Range(0, avoidanceWeightAbs);
        // currentAvoidanceDistance = UnityEngine.Random.Range(0, cohesionWeight);
    }

    private void SetNewCohesionWeight() => cohesionWeightAbs = UnityEngine.Random.Range(fishManager.minCohesionWeight, fishManager.maxCohesionWeight);

    private void SetNewAvoidanceWeight() => avoidanceWeightAbs = UnityEngine.Random.Range(fishManager.minAvoidanceWeight, fishManager.maxAvoidanceWeight);

    private void SetNewAvoidanceDistance() => avoidanceDistanceAbs = UnityEngine.Random.Range(fishManager.minAvoidanceDistance, fishManager.maxAvoidanceDistance);

    private void SetNewLoopTime() => loopTime = UnityEngine.Random.Range(fishManager.minLoopTime, fishManager.maxLoopTime);
}
