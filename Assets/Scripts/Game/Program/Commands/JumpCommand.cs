using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : Command
{
    public Vector3Int step = Vector3Int.forward;

    public override CommandState CreateState() => new JumpCommandState();
}
public class JumpCommandState : CommandState<JumpCommand>
{
    public override void Execute()
    {
        base.Execute();
        var player = GetPlayer();
        var targetPosition = player.position + player.rotation.Rotate(prefab.step);
        var block = game.levelLoader.GetBlockStateAtTop<PlatformBlockState>(targetPosition);
        if (block != null && Mathf.Abs(block.position.y - player.position.y) == 1)
        {
            player.JumpTo(block.position);
        }
    }
}
