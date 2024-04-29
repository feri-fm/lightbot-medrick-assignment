using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : BaseManager
{
    public LevelLoader levelLoader;
    public ProgramLoader programLoader;

    public bool isRunning { get; private set; }
    public bool isFinished { get; private set; }

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
        LoadLevel(GetSelectedLevel());
        gamePanel.OpenPanel();
    }

    public void LoadLevel(Level level)
    {
        isRunning = false;
        isFinished = false;
        levelLoader.LoadLevel(level);
        levelLoader.LoadBlocks();
        programLoader.LoadProgram(level.program);
        selectedProcedure = programLoader.program.procedures[0];
        MarkDirty();
    }

    public void Run()
    {
        isRunning = true;
        programLoader.ClearExecution();
        programLoader.InsertProcedure("main");
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

    public void CheckFinished()
    {
        if (!isFinished && !levelLoader.states.Any(e => e is LightBlockState && !((LightBlockState)e).isOn))
        {
            isFinished = true;
            programLoader.PauseExecution();
            MarkDirty();
        }
    }

    public void LoadNextLevel()
    {
        var nextLevel = config.GetNextLevel(levelLoader.level.key);
        if (nextLevel != null)
        {
            SetSelectedLevel(nextLevel);
            LoadLevel(nextLevel);
        }
        else
        {
            LoadMenuScene();
        }
    }
}
