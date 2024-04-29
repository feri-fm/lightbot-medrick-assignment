using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Block : PoolObject
{
    public string key;

    public LevelLoader loader { get; private set; }
    public BlockState state { get; private set; }

    public void Setup(LevelLoader loader, BlockState state)
    {
        this.loader = loader;
        this.state = state;
        UpdateTransform();
        Setup();
    }
    public void Remove()
    {
        OnRemoved();
    }

    protected virtual void Setup() { }
    protected virtual void OnRemoved() { }

    public override void Update()
    {
        base.Update();
        UpdateTransform();
    }

    public void UpdateTransform()
    {
        transform.position = loader.ground.GetPosition(state.position);
        transform.rotation = loader.ground.GetRotation(state.rotation);
    }

    public abstract BlockState CreateState();
}
public abstract class Block<TState> : Block where TState : BlockState
{
    public new TState state => base.state as TState;
}
public class BlockState
{
    public Vector3Int position;
    public BlockRotation rotation;

    public LevelLoader loader { get; private set; }
    public Block prefab { get; private set; }

    public string key => prefab.key;

    protected virtual void Setup() { }
    protected virtual void OnRemoved() { }

    public void Setup(LevelLoader loader, Block prefab)
    {
        this.loader = loader;
        this.prefab = prefab;
        position = loader.ground.GetPoint(prefab.transform.position);
        rotation = BlockRotation.FromAngle(prefab.transform.eulerAngles.y);
        Setup();
    }

    public void Remove()
    {
        OnRemoved();
    }
}
