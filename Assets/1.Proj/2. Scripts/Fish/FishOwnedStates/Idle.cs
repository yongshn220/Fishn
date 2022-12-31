using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishOwnedStates
{
    public class Idle : State<FishController>
    {
        public override void Enter(FishController entity)
        {
            Debug.Log("Idle Enter");
        }

        public override void Execute(FishController entity)
        {
            Debug.Log("Idle Execute");
            entity.ChangeState(FishState.Move);
        }

        public override void Exit(FishController entity)
        {
            Debug.Log("Idle Exit");
        }
    }
}