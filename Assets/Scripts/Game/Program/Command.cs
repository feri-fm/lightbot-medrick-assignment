using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public float executionTime = 0.5f;

    public abstract CommandState CreateState();
}
public abstract class CommandState
{
    public Command prefab { get; private set; }
    public ProcedureState procedure { get; private set; }

    public float executionTime => prefab.executionTime;

    public void Setup(Command prefab, ProcedureState procedure)
    {
        this.prefab = prefab;
        this.procedure = procedure;
        Setup();
    }
    public void Remove()
    {
        OnRemoved();
    }

    public virtual void Execute() { }
    protected virtual void Setup() { }
    protected virtual void OnRemoved() { }
}
