using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishOwnedStates
{
    public class Eat : State<FishMovement>
    {
        public override void Enter(FishMovement entity)
        {
            Debug.Log("Eat Enter");
        }

        public override void Execute(FishMovement entity)
        {
            Debug.Log("Eat Execute");
        }

        public override void Exit(FishMovement entity)
        {
            Debug.Log("Eat Exit");
        }
    }
}