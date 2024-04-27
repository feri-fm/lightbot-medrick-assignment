using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager
{
    public Level startLevel;
    public LevelLoader levelLoader;
    public ProgramLoader programLoader;

    public bool isRunning { get; private set; }

    public new static GameManager instance { get; private set; }

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    private void Start()
    {
        LoadLevel(startLevel);
    }

    public void LoadLevel(Level level)
    {
        isRunning = false;
        levelLoader.LoadLevel(level);
        levelLoader.LoadBlocks();
        programLoader.LoadProgram(level.program);
    }

    public void Run()
    {
        programLoader.QueueProcedure("main");
        programLoader.StartExecution();
    }
    public void Stop()
    {
        levelLoader.ReloadLevel();
        programLoader.Clear();
    }
}
