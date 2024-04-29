using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public Vector3Int step = Vector3Int.forward;

    public override CommandState CreateState() => new MoveCommandState();
}
public class MoveCommandState : CommandState<MoveCommand>
{
    public override void Execute()
    {
        base.Execute();
        var player = GetPlayer();
        var targetPosition = player.position + player.rotation.Rotate(prefab.step);
        var block = game.levelLoader.GetBlockStateAtTop<PlatformBlockState>(targetPosition);
        if (block != null && block.position.y == player.position.y)
        {
            player.position = targetPosition;
        }
    }
}
