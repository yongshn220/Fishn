using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FishState 
{
    Idle,
    Move,
    Eat
}
public class FishController : MonoBehaviour
{
    public Vector3 currentVelocity;
    public float smoothDamp;

    private State<FishController>[] states;
    private State<FishController> currentState;
    private StateMachine<FishController> stateMachine;

    private float speed;

    public FishManager fishManager;

    void Start()
    {
        Setup();
    }

    void Update()
    {
        stateMachine.Execute();
    }

    void Setup()
    {
        states = new State<FishController>[3];
        states[(int) FishState.Idle] = new FishOwnedStates.Idle();
        states[(int) FishState.Move] = new FishOwnedStates.Move();
        states[(int) FishState.Eat] = new FishOwnedStates.Eat();
        

        stateMachine = new StateMachine<FishController>();
        stateMachine.Setup(this, states[(int) FishState.Idle]);
    }

    public void ChangeState(FishState newState)
    {
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void AssignManager(FishManager _fishManager)
    {
        fishManager = _fishManager;
    }

    public void InitializeSpeed(float _speed)
    {
        speed = _speed;
    }
}
