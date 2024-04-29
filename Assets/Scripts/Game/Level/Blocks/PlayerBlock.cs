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
        if (state.moving)
        {
            body.position = body.position.MoveValue(transform.position, moveSpeed * Time.deltaTime, smooth * Time.deltaTime);
            body.rotation = body.rotation.MoveValue(transform.rotation, rotationSpeed * Time.deltaTime, smooth * Time.deltaTime);
            var curve = state.jumpingFlag ? jumpCurve : moveCurve;
            var lastPosition = loader.ground.GetPosition(state.lastPosition);
            var t = (body.position - lastPosition).magnitude / (transform.position - lastPosition).magnitude;
            heightAnchor.localPosition = Vector3.up * curve.Evaluate(t);

            if (heightAnchor.localPosition.magnitude < 0.001f
                && (body.position - transform.position).magnitude < 0.001f
                && (Vector3.Angle(body.forward, transform.forward) < 1))
            {
                state.StopMoving();
            }
        }
        else
        {
            body.position = transform.position;
            body.rotation = transform.rotation;
            heightAnchor.localPosition = Vector3.zero;
        }
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
    public bool moving;
    public bool jumpingFlag;
    public Vector3Int lastPosition;

    public void MoveTo(Vector3Int to)
    {
        StartMoving();
        jumpingFlag = false;
        lastPosition = position;
        position = to;
    }
    public void JumpTo(Vector3Int to)
    {
        StartMoving();
        jumpingFlag = true;
        lastPosition = position;
        position = to;
    }
    public void RotateTo(BlockRotation to)
    {
        StartMoving();
        rotation = to;
    }
    public void StartMoving()
    {
        moving = true;
    }
    public void StopMoving()
    {
        moving = false;
    }
}
