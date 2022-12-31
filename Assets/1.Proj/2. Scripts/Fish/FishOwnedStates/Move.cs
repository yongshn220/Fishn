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
        private Vector3 centerMovePoint;
        private Vector3 currentCohesionPoint;

        public override void Enter(FishController entity)
        {
            Debug.Log("Move Enter");
            currentEntity = entity;
            Debug.Log("Move Enter1");
            CalculateCenterMovePoint();
            Debug.Log("Move Enter2");
            FindCohesionMovePoints();
            Debug.Log("Move Enter3");
            FindAvoidanceMovePoints();
            Debug.Log("Move Enter4");
        }

        public override void Execute(FishController entity)
        {
            MoveFish();   
        }

        public override void Exit(FishController entity)
        {
            Debug.Log("Move Exit");
        }

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
        
        void MoveFish()
        {
            Vector3 cohesionVector = CalculateCohesionVector();
            Vector3 avoidanceVector = CalculateAvoidanceVector();

            Vector3 moveVector = cohesionVector;

            moveVector = Vector3.SmoothDamp(currentEntity.transform.forward, moveVector, ref currentEntity.currentVelocity, 0.5f);
            currentEntity.transform.forward = moveVector;
            currentEntity.transform.position += moveVector * Time.deltaTime;
        }

        // Calculated only one time (Enter)
        private void CalculateCenterMovePoint()
        {
            Debug.Log("1");
            Vector3 centerPoint = Vector3.zero; 
            Debug.Log("2");
            foreach (var point in currentEntity.fishManager.movePoints)
            {
            Debug.Log("3");
                centerPoint += point.transform.position;
            }
            Debug.Log("4");

            centerPoint /= currentEntity.fishManager.movePoints.Length;
            this.centerMovePoint = centerPoint;
            Debug.Log("5");
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
}