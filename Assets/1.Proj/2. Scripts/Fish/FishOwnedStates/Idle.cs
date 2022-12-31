using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace FishOwnedStates
{
    public class Idle : State<FishController>
    {
        FishController currentEntity;

        public override void Enter(FishController entity)
        {
            currentEntity = entity;
            DoIdleAction();
        }

        public override void Execute(FishController entity)
        {
        }

        public override void Exit(FishController entity)
        {
            Debug.Log("Idle Exit");
        }

        private void DoIdleAction()
        {   

            int randomTime = UnityEngine.Random.Range(1, 4) * 1000;
            Debug.Log(randomTime);
            Task.Delay(randomTime).ContinueWith((task) => {
                ChangeState(FishState.Move);
            });
        }

        private void ChangeState(FishState newState)
        {
            currentEntity.ChangeState(newState);
        }
    }
}