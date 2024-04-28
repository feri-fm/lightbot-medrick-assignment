using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager
{
    public Level startLevel;
    public LevelLoader levelLoader;
    public ProgramLoader programLoader;

    public bool isRunning { get; private set; }

    public ProcedureState selectedProcedure;

    public GamePanel gamePanel => panelGroup.GetPanel<GamePanel>();

    public new static GameManager instance { get; private set; }

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    private void Start()
    {
        LoadLevel(startLevel);
        gamePanel.OpenPanel();
    }

    public void LoadLevel(Level level)
    {
        isRunning = false;
        levelLoader.LoadLevel(level);
        levelLoader.LoadBlocks();
        programLoader.LoadProgram(level.program);
        selectedProcedure = programLoader.program.procedures[0];
        MarkDirty();
    }

    public void Run()
    {
        isRunning = true;
        programLoader.QueueProcedure("main");
        programLoader.StartExecution();
        MarkDirty();
    }
    public void Stop()
    {
        isRunning = false;
        levelLoader.ReloadLevel();
        levelLoader.LoadBlocks();
        programLoader.StopExecution();
        MarkDirty();
    }

    public void SelectProcedure(ProcedureState procedure)
    {
        selectedProcedure = procedure;
        MarkDirty();
    }
    public void AddCommand(Command command)
    {
        selectedProcedure.AddCommand(command);
        MarkDirty();
    }
    public void RemoveCommand(CommandState command)
    {
        command.procedure.RemoveCommand(command);
        MarkDirty();
    }
}
