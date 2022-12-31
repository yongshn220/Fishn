using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishOwnedStates
{
    public class Eat : State<FishController>
    {
        public override void Enter(FishController entity)
        {
            Debug.Log("Eat Enter");
        }

        public override void Execute(FishController entity)
        {
            Debug.Log("Eat Execute");
        }

        public override void Exit(FishController entity)
        {
            Debug.Log("Eat Exit");
        }
    }
}