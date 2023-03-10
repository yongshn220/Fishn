using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace FishOwnedStates
{
    public class Idle : State<FishMovement>
    {
        private FishMovement fishMovement;

        private float stateTime;
        private float curStateTime = 0;

        public override void Enter(FishMovement fishMovement)
        {
            this.fishMovement = fishMovement;
            stateTime = UnityEngine.Random.Range(0, 3);
        }

        public override void Execute(FishMovement fishMovement)
        {
            if (curStateTime >= stateTime)
            {
                ChangeState();
            }

            curStateTime += Time.deltaTime;
        }  

        public override void Exit(FishMovement fishMovement)
        {
            ResetState();
        }

        private void ResetState()
        {
            fishMovement = null;
            curStateTime = 0;
        }

        private void ChangeState()
        {
            int rnum = UnityEngine.Random.Range(1,3);
            
            if (rnum == 1) { fishMovement.ChangeState(FishState.Move); return; }
            
            if (rnum == 2) 
            { 
                EntityMono mono = fishMovement.GetComponent<EntityMono>();
                if (mono.feed < mono.maxFeed)
                    fishMovement.ChangeState(FishState.Eat); return; 
            }
        }
    }
}