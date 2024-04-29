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

    public ProcedureState selectedProcedure { get; private set; }

    public Level level => levelLoader.level;

    public GamePanel gamePanel => panelGroup.GetPanel<GamePanel>();

    public new static GameManager instance { get; private set; }

    private ProgramData programData;

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
        LoadProgram();
        MarkDirty();
    }

    public void Run()
    {
        isRunning = true;
        programLoader.ClearExecution();
        programLoader.InsertProcedure("main");
        programLoader.StartExecution();
        StoreProgram();
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
    public void ClearProgram()
    {
        programLoader.Clear();
        MarkDirty();
    }

    public void CheckFinished()
    {
        if (!isFinished && !levelLoader.states.Any(e => e is LightBlockState && !((LightBlockState)e).isOn))
        {
            isFinished = true;
            programLoader.PauseExecution();
            SaveProgram();
            MarkDirty();
        }
    }

    public void LoadNextLevel()
    {
        var nextLevel = config.GetNextLevel(level.key);
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

    private string GetProgramDataKey()
    {
        return $"prg_{level.key}";
    }
    public void StoreProgram()
    {
        programData = programLoader.program.Save();
    }
    public void SaveProgram()
    {
        PlayerPrefs.SetString(GetProgramDataKey(), programData.ToJson());
    }
    public void LoadProgram()
    {
        if (PlayerPrefs.HasKey(GetProgramDataKey()))
        {
            var data = PlayerPrefs.GetString(GetProgramDataKey()).FromJson<ProgramData>();
            programLoader.program.Load(data);
        }
    }
}
