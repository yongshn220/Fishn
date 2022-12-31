using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishOwnedStates
{
    public class Move : State<FishController>
    {
        private FishController currentEntity;
        private List<MovePointController> cohesionMovePoints = new List<MovePointController>();
        private List<MovePointController> avoidanceMovePoints = new List<MovePointController>();

        private Vector3 currentCohesionPoint;

        private Vector3 centerMovePoint;

        private float currentSmoothDampTime;
        private int smoothDampDir = 1; // -1 or 1

        private Vector3 currentVelocity;
        private float currentSpeed;
        private int speedDir = 1; // -1 or 1

#region Main
        public override void Enter(FishController entity)
        {
            currentEntity = entity;
            Initialize();
            CalculateCenterMovePoint();
            FindCohesionMovePoints();
            FindAvoidanceMovePoints();
        }

        public override void Execute(FishController entity)
        {
            MoveFish();   
            CheckTransitionToIdle();
        }

        public override void Exit(FishController entity)
        {
            Debug.Log("Move Exit");
        }
#endregion


#region Initalize
        private void Initialize()
        {
            currentSmoothDampTime = UnityEngine.Random.Range(currentEntity.fishManager.minSmoothDampTime, currentEntity.fishManager.maxSmoothDampTime);

            currentSpeed = UnityEngine.Random.Range(currentEntity.fishManager.minSpeed, currentEntity.fishManager.maxSpeed);
        }
#endregion


#region Finding
        private void FindCohesionMovePoints()
        {
            cohesionMovePoints.Clear();
            foreach (var point in currentEntity.fishManager.movePoints)
            {   
                cohesionMovePoints.Add(point);
            }
        }

        private void FindAvoidanceMovePoints()
        {
            avoidanceMovePoints.Clear();
            foreach (var point in currentEntity.fishManager.movePoints)
            {
                avoidanceMovePoints.Add(point);
            }
        }
#endregion


#region Calculation
        void MoveFish()
        {
            CalculateSmoothDampTime();
            CalculateCurrentSpeed();

            Vector3 cohesionVector = CalculateCohesionVector();
            Vector3 avoidanceVector = CalculateAvoidanceVector();

            Vector3 moveVector = cohesionVector;

            Debug.Log(currentSpeed);

            moveVector = Vector3.SmoothDamp(currentEntity.transform.forward, moveVector, ref currentVelocity, currentSmoothDampTime);
            currentEntity.transform.forward = moveVector;
            currentEntity.transform.position += moveVector * currentSpeed * Time.deltaTime;
        }

        // Calculated only one time (Enter)
        private void CalculateCenterMovePoint()
        {
            Vector3 centerPoint = Vector3.zero; 
            foreach (MovePointController point in currentEntity.fishManager.movePoints)
            {
                centerPoint += point.transform.position;
            }

            centerPoint /= currentEntity.fishManager.movePoints.Length;
            this.centerMovePoint = centerPoint;
        }

        private Vector3 CalculateCohesionVector()
        {
            Vector3 cohesionVector = Vector3.zero;
            
            foreach (var point in cohesionMovePoints)
            {
                cohesionVector += point.transform.position + ((this.centerMovePoint - point.transform.position) * (point.currentCohesionWeight));
                // cohesionVector += point.transform.position; //test
            }

            cohesionVector /= cohesionMovePoints.Count;
            
            currentEntity.fishManager.testObject.transform.position = cohesionVector; //test 
            this.currentCohesionPoint = cohesionVector;

            cohesionVector -= currentEntity.transform.position;

            cohesionVector = cohesionVector.normalized;

            currentEntity.testVector1 = cohesionVector;

            return cohesionVector;
        }

        private Vector3 CalculateAvoidanceVector()
        {
            Vector3 avoidanceVector = Vector3.zero;

            foreach (var point in avoidanceMovePoints)
            {
                avoidanceVector += (currentEntity.transform.position - point.transform.position) * point.currentAvoidanceWeight;
            }

            avoidanceVector /= avoidanceMovePoints.Count;
            avoidanceVector = avoidanceVector.normalized;
            return avoidanceVector;
        }

        private void CalculateSmoothDampTime()
        {
            float smoothDampDifference = currentEntity.fishManager.maxSmoothDampTime - currentEntity.fishManager.minSmoothDampTime;
            currentSmoothDampTime += smoothDampDir * (smoothDampDifference / currentEntity.fishManager.smoothDampLoopTime) * Time.deltaTime;

            if (currentSmoothDampTime >= currentEntity.fishManager.maxSmoothDampTime) smoothDampDir = -1;
            if (currentSmoothDampTime <= currentEntity.fishManager.minSmoothDampTime) smoothDampDir = 1;
        }

        private void CalculateCurrentSpeed()
        {
            float speedDifference = currentEntity.fishManager.maxSpeed - currentEntity.fishManager.minSpeed;
            currentSpeed += speedDir * (speedDifference / currentEntity.fishManager.speedLoopTime) * Time.deltaTime;

            if (currentSpeed >= currentEntity.fishManager.maxSpeed) speedDir = -1;
            if (currentSpeed <= currentEntity.fishManager.minSpeed) speedDir = 1;
        }
#endregion


#region Check
        private void CheckTransitionToIdle()
        {
            if (IsEntityInDistanceFromTarget()) ChangeState(FishState.Idle);
        }

        private bool IsEntityInDistanceFromTarget()
        {
            float currentDistanceSqr = Vector3.SqrMagnitude(currentEntity.transform.position - currentCohesionPoint);

            return (currentDistanceSqr <= currentEntity.fishManager.minTargetdistance);
        }

        private void ChangeState(FishState newState)
        {
            currentEntity.ChangeState(newState);
        }
    }
#endregion
}