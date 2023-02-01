using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishOwnedStates
{
    public class Eat : State<FishMovement>
    {
        private FishManager fishManager;

        private CoralPlantMono targetCoralPlantMono;
        private FishMovement fishMovement;

        private Vector3 currentVelocity;
        private float speed;
        private float SmoothTime = 1.0f;

        private float reachDistanceSqr = 3.0f;
        private bool isReachCoral = false;
        
        private float eatTime = 3.0f;
        private float curEatTime = 0;

        public override void Enter(FishMovement fishMovement)
        {
            this.fishManager = fishMovement.fishManager;
            this.fishMovement = fishMovement;
            this.speed = UnityEngine.Random.Range(fishManager.minSpeed, fishManager.maxSpeed);
            
            var coralPlantMonoList = GameManager.instance.viewSceneManager.GetEnabledCoralPlantMonoList();
            if (coralPlantMonoList.Count == 0) fishMovement.ChangeState(FishState.Idle);
            this.targetCoralPlantMono = SelectNearestCoral(coralPlantMonoList);
        }

        public override void Execute(FishMovement fishMovement)
        {
            if (!TryMoveTowardCoral()) return; 
            EatCoral();
        }

        public override void Exit(FishMovement fishMovement)
        {
            SetAnimatorEat(false);
            Reset();
        }

#region Setup
        private void SetAnimatorEat(bool state)
        {
            fishMovement.GetComponent<EntityAnimatorController>()?.SetBoolAnimator(AnimatorType.IsEat, state);
        }

        private CoralPlantMono SelectNearestCoral(List<CoralPlantMono> coralPlantMonoList)
        {
            CoralPlantMono selectedCoralPlantMono = coralPlantMonoList[0];
            float minDistance = float.MaxValue;
            foreach (CoralPlantMono mono in coralPlantMonoList)
            {
                float distance = Vector3.Distance(fishMovement.transform.position, mono.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    selectedCoralPlantMono = mono;
                }
            }
            return selectedCoralPlantMono;
        }
#endregion
        private void EatCoral()
        {
            if (curEatTime < eatTime)
            {
                curEatTime += Time.deltaTime;
                return;
            }

            EntityMono mono = fishMovement.GetComponent<EntityMono>();
            mono.GetFeed(targetCoralPlantMono.unitCoral);
            fishMovement.ChangeState(FishState.Move);
        }

        private bool TryMoveTowardCoral()
        {
            if (isReachCoral) return true;
            if (targetCoralPlantMono == null) return false;

            Vector3 moveVector = targetCoralPlantMono.transform.position - fishMovement.transform.position;

            moveVector = Vector3.SmoothDamp(fishMovement.transform.forward, moveVector, ref currentVelocity, SmoothTime, 1);

            fishMovement.transform.forward = moveVector;
            fishMovement.transform.position += moveVector * speed * Time.deltaTime;

            CheckIfReachCoral();
            return false;
        }

        private void CheckIfReachCoral()
        {
            float distSqr = Mathf.Pow(Vector3.Distance(fishMovement.transform.position, targetCoralPlantMono.transform.position), 2);

            if (distSqr < reachDistanceSqr)
            {
                isReachCoral = true;
                SetAnimatorEat(true);
            }   
        }

        private void Reset()
        {
            fishMovement = null;
            targetCoralPlantMono = null;
            curEatTime = 0;
            isReachCoral = false;
        }
    }
}

/*
    1. Find near coral.
        1. Get enabled coral plant list from tank controller.
        2. 
    2. move to the coral.
    3. stay idle, and eat (eat motion)

*/