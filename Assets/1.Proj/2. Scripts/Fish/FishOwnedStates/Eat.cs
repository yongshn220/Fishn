using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishOwnedStates
{
    public class Eat : State<FishMovement>
    {
        private FishManager fishManager;

        private CoralPlantMono targetCoralPlantMono;
        private FishMovement curFishMovement;

        private Vector3 currentVelocity;
        private float speed;
        private float SmoothTime = 1.0f;

        private float reachDistanceSqr = 3.0f;
        private bool isReachCoral = false;
        

        public override void Enter(FishMovement fishMovement)
        {
            this.fishManager = fishMovement.fishManager;
            this.curFishMovement = fishMovement;
            this.speed = UnityEngine.Random.Range(fishManager.minSpeed, fishManager.maxSpeed);
            
            var coralPlantMonoList = GameManager.instance.viewSceneManager.GetEnabledCoralPlantMonoList();
            this.targetCoralPlantMono = SelectNearestCoral(coralPlantMonoList);
        }

        public override void Execute(FishMovement fishMovement)
        {
            TryEatCoral();
        }

        public override void Exit(FishMovement fishMovement)
        {
        }

        private CoralPlantMono SelectNearestCoral(List<CoralPlantMono> coralPlantMonoList)
        {
            Debug.Log("a");
            CoralPlantMono selectedCoralPlantMono = coralPlantMonoList[0];
            float minDistance = float.MaxValue;
            foreach (CoralPlantMono mono in coralPlantMonoList)
            {
                float distance = Vector3.Distance(curFishMovement.transform.position, mono.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    selectedCoralPlantMono = mono;
                }
            }
            return selectedCoralPlantMono;
        }

        private void TryEatCoral()
        {
            if (!isReachCoral) MoveTowardCoral(); return;


        }

        private void MoveTowardCoral()
        {
            if (targetCoralPlantMono == null) return;

            Vector3 moveVector = targetCoralPlantMono.transform.position - curFishMovement.transform.position;

            Vector3.SmoothDamp(curFishMovement.transform.position, targetCoralPlantMono.transform.position, ref currentVelocity, SmoothTime, 3);

            curFishMovement.transform.position += moveVector * speed * Time.deltaTime;

            float distSqr = Mathf.Pow(Vector3.Distance(curFishMovement.transform.position, targetCoralPlantMono.transform.position), 2);

            if (distSqr < reachDistanceSqr)
            {
                isReachCoral = true;
            }
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