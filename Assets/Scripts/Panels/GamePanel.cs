using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel
{
    public GameObjectMember running;
    public GameObjectMember stopped;
    public ListLoaderMember commands;
    public ListLoaderMember procedures;

    public override void OnRender()
    {
        base.OnRender();
        running.SetActive(game.isRunning);
        stopped.SetActive(!game.isRunning);
        commands.Setup(game.levelLoader.level.commands);
        procedures.Setup(game.programLoader.program.procedures);
    }

    [Member]
    public void Run()
    {
        game.Run();
    }

    [Member]
    public void Stop()
    {
        game.Stop();
    }
}
