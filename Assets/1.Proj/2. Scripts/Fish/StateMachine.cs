using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private T ownerEntity;
    private State<T> currentState;
    public void Setup(T owner, State<T> entryState)
    {
        ownerEntity = owner;
        currentState = entryState;
    }

    public void Execute()
    {
        if (currentState == null) return; 
        
        currentState.Execute(ownerEntity);
    }

    public void ChangeState(State<T> newState)
    {
        if (newState == null) return;

        if (currentState != null)
        {
            currentState.Exit(ownerEntity);
        }
        Debug.Log("[SM] Change State " + newState);
        currentState = newState;
        currentState.Enter(ownerEntity);
    }
}
