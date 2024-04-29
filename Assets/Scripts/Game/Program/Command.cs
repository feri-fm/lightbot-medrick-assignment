using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public string key;
    public float executionTime = 0.5f;

    public Member<PoolObject> view;

    public void _Setup()
    {
        MemberBinder.BindForce(this);
    }

    public PoolObject SpawnView(ObjectPool pool, Transform parent)
    {
        var obj = pool.Spawn(view.value, parent);
        var rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        return obj;
    }

    public abstract CommandState CreateState();
}
public abstract class CommandState
{
    public Command prefab { get; private set; }
    public ProcedureState procedure { get; private set; }

    public float executionTime => prefab.executionTime;

    public GameManager game => GameManager.instance;

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
    public virtual void ExecuteEnd() { }
    protected virtual void Setup() { }
    protected virtual void OnRemoved() { }

    public PlayerBlockState GetPlayer()
    {
        return game.levelLoader.GetBlockState<PlayerBlockState>();
    }

    public CommandData Save()
    {
        return new CommandData()
        {
            key = prefab.key,
        };
    }
    public void Load(CommandData data)
    {

    }
}
public abstract class CommandState<T> : CommandState where T : Command
{
    public new T prefab => base.prefab as T;
}

public class CommandData
{
    public string key;
}
