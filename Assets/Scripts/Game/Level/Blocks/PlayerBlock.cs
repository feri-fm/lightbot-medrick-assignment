using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : Block<PlayerBlockState>
{
    public Transform body;
    public Transform heightAnchor;
    public float moveSpeed = 5;
    public float rotationSpeed = 500;
    public float smooth = 8;
    public AnimationCurve moveCurve;
    public AnimationCurve jumpCurve;

    protected override void Setup()
    {
        base.Setup();
        body.position = transform.position;
        body.rotation = transform.rotation;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        body.position = body.position.MoveValue(transform.position, moveSpeed * Time.deltaTime, smooth * Time.deltaTime);
        body.rotation = body.rotation.MoveValue(transform.rotation, rotationSpeed * Time.deltaTime, smooth * Time.deltaTime);
        var curve = state.jumping ? jumpCurve : moveCurve;
        var lastPosition = loader.ground.GetPosition(state.lastPosition);
        var t = (body.position - lastPosition).magnitude / (transform.position - lastPosition).magnitude;
        heightAnchor.localPosition = Vector3.up * curve.Evaluate(t);
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
    public bool jumping;
    public Vector3Int lastPosition;

    public void MoveTo(Vector3Int to)
    {
        jumping = false;
        lastPosition = position;
        position = to;
    }
    public void JumpTo(Vector3Int to)
    {
        jumping = true;
        lastPosition = position;
        position = to;
    }
}
