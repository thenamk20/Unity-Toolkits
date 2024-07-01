using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBase<T> : MonoBehaviour where T : StateMachineBase<T>
{
    [ShowInInspector]
    protected StateBase<T> currentState;

    protected virtual void Update()
    {
        currentState?.OnUpdate();
    }

    public void ChangeState(StateBase<T> newState)
    {
        currentState?.OnEnd();
        currentState = newState;
        currentState.OnStart();
    }
}