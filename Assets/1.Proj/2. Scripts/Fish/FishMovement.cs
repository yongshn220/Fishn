using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FishState 
{
    Idle,
    Move,
    Eat
}
public class FishMovement : MonoBehaviour
{
    private State<FishMovement>[] states;
    private State<FishMovement> currentState;
    private StateMachine<FishMovement> stateMachine;

    private float speed;
    public FishManager fishManager;
    public Vector3 testVector1;

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
        states = new State<FishMovement>[3];
        states[(int) FishState.Idle] = new FishOwnedStates.Idle();
        states[(int) FishState.Move] = new FishOwnedStates.Move();
        states[(int) FishState.Eat] = new FishOwnedStates.Eat();

        stateMachine = new StateMachine<FishMovement>();
        stateMachine.Setup(this, states[(int) FishState.Idle]); // Set Default state (can be any state).
        ChangeState(FishState.Idle); // Set Default again to call Enter() function.
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
