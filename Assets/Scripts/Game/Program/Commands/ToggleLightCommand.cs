using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLightCommand : Command
{
    public override CommandState CreateState() => new ToggleLightCommandState();
}
public class ToggleLightCommandState : CommandState<ToggleLightCommand>
{
    public override void Execute()
    {
        base.Execute();
        var player = GetPlayer();
        var light = game.levelLoader.GetBlockStateAt<LightBlockState>(player.position);
        if (light != null)
        {
            light.ToggleLight();
            game.CheckFinished();
        }
    }
}
