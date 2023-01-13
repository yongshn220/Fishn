using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FishOwnedStates
{
    public class Move : State<FishMovement>
    {
        private FishMovement currentFishMovement;
        private FishManager fishManager;

        private List<Transform> selectedMovePointList = new List<Transform>();
        private List<Transform> unSelectedMovePointList = new List<Transform>();
        private Vector3 movePointCenter;
        private Vector3 targetArea; // minimum area from the center to the selected movepoints. (values are Relative)
        private Vector3 target;
        
        private float currentTargetAreaX = 0; // current delta 'x' from the movepoint center. (value is relative from center)
        private float currentTargetAreaY = 0; // current delta 'y' from the movepoint center. (value is relative from center)
        private float targetAreaXDir = 1;
        private float targetAreaYDir = 1;
        private float targetTimeX;  // How fast the target moves in x-direction inside the movepoint area.
        private float targetTimeY; // How fast the target moves in y-direction inside the movepoint area.

        private float movePointTime; // How frequently update the selectedMovePoint.
        private float currentMovePointTime = 0;

        private float currentSmoothDampTime; // How smoothly turn the fish.
        private int smoothDampDir = 1; // -1 or 1

        private Vector3 currentVelocity;
        private float currentSpeed;
        private int speedDir = 1; // -1 or 1

        private GameObject testObject;

#region Main
        public override void Enter(FishMovement fishMovement)
        {
            currentFishMovement = fishMovement;
            fishManager = fishMovement.fishManager;
            testObject = Transform.Instantiate(fishManager.testObject);
            Setup();
        }

        public override void Execute(FishMovement fishMovement)
        {
            UpdateDeltaDynamics();
            MoveFish();
            testObject.transform.position = target;
        }

        public override void Exit(FishMovement fishMovement)
        {
            Debug.Log("Move Exit");
        }
#endregion


#region Setup
        private void Setup()
        {
            SetupTime();
            SetupMovePoint();
            SetMovePointCenter();
            SetMovePointArea();
        }

        private void SetupTime()
        {
            movePointTime = UnityEngine.Random.Range(fishManager.minMovePointTime, fishManager.maxMovePointTime);
            targetTimeX = UnityEngine.Random.Range(fishManager.minMovePointTargetTime, fishManager.maxMovePointTargetTime);
            targetTimeY = UnityEngine.Random.Range(fishManager.minMovePointTargetTime, fishManager.maxMovePointTargetTime);

            currentSmoothDampTime = UnityEngine.Random.Range(fishManager.minSmoothDampTime, fishManager.maxSmoothDampTime);
            currentSpeed = UnityEngine.Random.Range(fishManager.minSpeed, fishManager.maxSpeed);
        }

        // Insert random Move Points 
        private void SetupMovePoint()
        {
            int num = fishManager.selectedMovePointNum;
            unSelectedMovePointList = fishManager.movePoints.shallowCopy();

            Transform randomSelectedPoint = SelectOneRandomInList(unSelectedMovePointList); // Random Select one.
            TestSetIcon(randomSelectedPoint.gameObject);
            selectedMovePointList.Add(randomSelectedPoint);                                 // Add 1 random position
            unSelectedMovePointList.Remove(randomSelectedPoint);                            // Remove from unSelectedList.

            unSelectedMovePointList.SortByDistance(randomSelectedPoint.position);           // Sort the unselectedList depends on the distance from the above selected position.

            for (int i = 0; i < num - 1; i++)               // Take rest from the nearest.
            {
                selectedMovePointList.Add(unSelectedMovePointList[i]);
                TestSetIcon(unSelectedMovePointList[i].gameObject);
                unSelectedMovePointList.RemoveAt(i);
            }
        }
        
        private void TestSetIcon(GameObject gameObject)
        {
#if UNITY_EDITOR
            var iconContent = EditorGUIUtility.IconContent("sv_icon_dot11_pix16_gizmo");
            EditorGUIUtility.SetIconForObject(gameObject, (Texture2D) iconContent.image);
#endif
        }

        private Transform SelectOneRandomInList(List<Transform> list)
        {
            int randomInd = UnityEngine.Random.Range(0, list.Count);
            Transform randomSelected = list[randomInd];
            return randomSelected;
        }

        // Calculated once per movepoint time.
        private void SetMovePointCenter()
        {
            this.movePointCenter = selectedMovePointList.GetCenter();
        }

        private void SetMovePointArea()
        {
            if (this.movePointCenter != null)
            {
                this.targetArea = selectedMovePointList.GetAreaFromCenter();
            }
            else Debug.Assert(true, "movePointCenter is null.");
        }
#endregion


#region Action
        private void MoveFish()
        {
            UpdateTargetPosition();

            Vector3 moveVecter = target - currentFishMovement.transform.position;
            moveVecter = Vector3.SmoothDamp(currentFishMovement.transform.forward, moveVecter, ref currentVelocity, currentSmoothDampTime);
            currentFishMovement.transform.forward = moveVecter;
            currentFishMovement.transform.position += moveVecter * currentSpeed * Time.deltaTime;

        }

        private void UpdateTargetPosition()
        {
            target = new Vector3(movePointCenter.x + currentTargetAreaX, movePointCenter.y, movePointCenter.z);
        }

        // Called once per every movePointTime.
        private void SelectNewMovePoint()
        {
            if (unSelectedMovePointList.Count <= 0)
            {
                SetupMovePoint();
            }

            unSelectedMovePointList.SortByDistance(movePointCenter);
            selectedMovePointList.RemoveAt(0);
            selectedMovePointList.Add(unSelectedMovePointList[0]);

            TestSetIcon(unSelectedMovePointList[0].gameObject);
            unSelectedMovePointList.RemoveAt(0);
        }
#endregion

#region Update Delta Dynamics
        // Dynamic data that calculated every delta time.
        private void UpdateDeltaDynamics()
        {
            UpdateCurrentMovePointTime();
            UpdateCurrentTargetAreaX();
            UpdateCurrentTargetAreaY();
            UpdateSmoothDampTime();
            UpdateCurrentSpeed();
        }

        // Update move point target (dynamic x position)
        private void UpdateCurrentTargetAreaX()
        {
            currentTargetAreaX += targetAreaXDir * (targetArea.x / targetTimeX) * Time.deltaTime;
            if (currentTargetAreaX >= targetArea.x)
            {
                targetAreaXDir = -1;
                targetTimeX = UnityEngine.Random.Range(fishManager.minMovePointTargetTime, fishManager.maxMovePointTargetTime);

            }
            if (currentTargetAreaX <= -targetArea.x)
            {
                targetAreaXDir = 1;
                targetTimeX = UnityEngine.Random.Range(fishManager.minMovePointTargetTime, fishManager.maxMovePointTargetTime);
            }
        }

        // Update move point target (dynamic y position)
        private void UpdateCurrentTargetAreaY()
        {
            currentTargetAreaY += targetAreaYDir * (targetArea.y / targetTimeY) * Time.deltaTime;
            if (currentTargetAreaY >= targetArea.y)
            {
                targetAreaYDir = -1;
                targetTimeY = UnityEngine.Random.Range(fishManager.minMovePointTargetTime, fishManager.maxMovePointTargetTime);

            }
            if (currentTargetAreaY <= -targetArea.y)
            {
                targetAreaYDir = 1;
                targetTimeY = UnityEngine.Random.Range(fishManager.minMovePointTargetTime, fishManager.maxMovePointTargetTime);
            }
        }
        
        // Update Move Point Time
        private void UpdateCurrentMovePointTime()
        {
            currentMovePointTime += Time.deltaTime;

            if (currentMovePointTime >= movePointTime)
            {
                currentMovePointTime = 0;
                movePointTime = UnityEngine.Random.Range(fishManager.minMovePointTime, fishManager.maxMovePointTime);
                SelectNewMovePoint();
                SetMovePointCenter();
                SetMovePointArea();
            }
        }

        private void UpdateSmoothDampTime()
        {
            float smoothDampDifference = fishManager.maxSmoothDampTime - fishManager.minSmoothDampTime;
            currentSmoothDampTime += smoothDampDir * (smoothDampDifference / fishManager.smoothDampLoopTime) * Time.deltaTime;

            if (currentSmoothDampTime >= fishManager.maxSmoothDampTime) smoothDampDir = -1;
            if (currentSmoothDampTime <= fishManager.minSmoothDampTime) smoothDampDir = 1;
        }

        private void UpdateCurrentSpeed()
        {
            float speedDifference = fishManager.maxSpeed - fishManager.minSpeed;
            currentSpeed += speedDir * (speedDifference / fishManager.speedLoopTime) * Time.deltaTime;

            if (currentSpeed >= fishManager.maxSpeed) speedDir = -1;
            if (currentSpeed <= fishManager.minSpeed) speedDir = 1;
        }
#endregion


#region Check
        private bool IsEntityInDistanceFromTarget()
        {
            // float currentDistanceSqr = Vector3.SqrMagnitude(currentFishMovement.transform.position - currentCohesionPoint);

            // return (currentDistanceSqr <= fishManager.minTargetdistance);
            return false;
        }

        private void ChangeState(FishState newState)
        {
            currentFishMovement.ChangeState(newState);
        }
    }
#endregion
}