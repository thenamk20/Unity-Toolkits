using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase<T> where T :StateMachineBase<T>
{
    protected T context;

    public StateBase(T context)
    {
        this.context = context;
    }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnEnd() { }
}