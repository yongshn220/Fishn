using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace FishOwnedStates
{
    public class Idle : State<FishMovement>
    {
        FishMovement currentEntity;

        bool isTransitToMoveReady = false;

        public override void Enter(FishMovement entity)
        {
            ResetState();
            currentEntity = entity;
            DoIdleAction();
        }

        public override void Execute(FishMovement entity)
        {
            if (isTransitToMoveReady)
            {
                entity.ChangeState(FishState.Move);
            }
        }  

        public override void Exit(FishMovement entity)
        {
            ResetState();
        }

        private void DoIdleAction()
        {   
            int randomTime = UnityEngine.Random.Range(1, 4) * 1000;
            Task.Delay(randomTime).ContinueWith((task) => {
                isTransitToMoveReady = true;
            });
        }

        private void ResetState()
        {
            isTransitToMoveReady = false;
            currentEntity = null;
        }
    }
}