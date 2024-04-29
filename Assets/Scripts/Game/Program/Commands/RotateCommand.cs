using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : Command
{
    public int rotation;
    public override CommandState CreateState() => new RotateCommandState();
}
public class RotateCommandState : CommandState<RotateCommand>
{
    public override void Execute()
    {
        base.Execute();
        var player = GetPlayer();
        player.RotateTo(player.rotation + prefab.rotation);
    }
}
