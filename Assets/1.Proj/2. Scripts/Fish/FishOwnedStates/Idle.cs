using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace FishOwnedStates
{
    public class Idle : State<FishController>
    {
        FishController currentEntity;

        bool isTransitToMoveReady = false;

        public override void Enter(FishController entity)
        {
            Debug.Log("idle enter");
            ResetState();
            currentEntity = entity;
            DoIdleAction();
        }

        public override void Execute(FishController entity)
        {
            if (isTransitToMoveReady)
            {
                entity.ChangeState(FishState.Move);
            }
        }  

        public override void Exit(FishController entity)
        {
            Debug.Log("idle exit");
            ResetState();
        }

        private void DoIdleAction()
        {   
            int randomTime = UnityEngine.Random.Range(1, 4) * 1000;
            Debug.Log(randomTime);
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