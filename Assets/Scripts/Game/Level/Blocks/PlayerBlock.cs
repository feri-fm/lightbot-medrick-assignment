using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : Block<PlayerBlockState>
{
    public Transform body;
    public float moveSpeed = 5;
    public float rotationSpeed = 500;
    public float smooth = 8;

    protected override void Setup()
    {
        base.Setup();
        body.position = transform.position;
        body.rotation = transform.rotation;
    }

    public override void Update()
    {
        base.Update();
        body.position = body.position.MoveValue(transform.position, moveSpeed * Time.deltaTime, smooth * Time.deltaTime);
        body.rotation = body.rotation.MoveValue(transform.rotation, rotationSpeed * Time.deltaTime, smooth * Time.deltaTime);
    }

    public override void OnSpawned()
    {
        base.OnSpawned();
        body.parent = transform.parent;
    }
    public override void OnPooled()
    {
        base.OnPooled();
        body.parent = transform;
    }
    public override void OnDestroyed()
    {
        base.OnDestroyed();
        body.parent = transform;
    }

    public override BlockState CreateState() => new PlayerBlockState();
}
public class PlayerBlockState : BlockState
{

}
