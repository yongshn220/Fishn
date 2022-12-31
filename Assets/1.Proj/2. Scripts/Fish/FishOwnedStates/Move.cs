using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishOwnedStates
{
    public class Move : State<FishController>
    {
        private Vector3 centerMovePoint;
        private List<MovePointController> cohesionMovePoints = new List<MovePointController>();
        private List<MovePointController> avoidanceMovePoints = new List<MovePointController>();

        private FishController currentEntity;
        public override void Enter(FishController entity)
        {
            currentEntity = entity;
            CalculateCenterMovePoint();
            FindCohesionMovePoints();
            FindAvoidanceMovePoints();
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

            //moveVector = Vector3.SmoothDamp(currentEntity.transform.position, moveVector, ref currentEntity.currentVelocity, currentEntity.smoothDamp);
            currentEntity.transform.forward = moveVector;
            currentEntity.transform.position += moveVector * Time.deltaTime;
        }

        // Calculated only one time (Enter)
        private void CalculateCenterMovePoint()
        {
            Vector3 centerPoint = Vector3.zero; 
            foreach (var point in currentEntity.fishManager.movePoints)
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

            cohesionVector -= currentEntity.transform.position;
            cohesionVector = cohesionVector.normalized;
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
    }
}