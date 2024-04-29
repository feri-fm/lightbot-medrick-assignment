using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel
{
    public GameObjectMember running;
    public GameObjectMember stopped;
    public ListLoaderMember commands;
    public ListLoaderMember procedures;
    public GameObjectMember finished;
    public TextMember levelKey;

    public override void OnRender()
    {
        base.OnRender();
        running.SetActive(game.isRunning);
        stopped.SetActive(!game.isRunning);
        commands.Setup(game.levelLoader.level.commands);
        procedures.Setup(game.programLoader.program.procedures);
        finished.SetActive(game.isFinished);
        levelKey.text = game.levelLoader.level.key;
    }

    [Member]
    public void Menu()
    {
        manager.LoadMenuScene();
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

    [Member]
    public void ClearProgram()
    {
        game.ClearProgram();
    }

    [Member]
    public void SaveProgram()
    {
        game.StoreProgram();
        game.SaveProgram();
    }

    [Member]
    public void NextLevel()
    {
        game.LoadNextLevel();
    }
}
