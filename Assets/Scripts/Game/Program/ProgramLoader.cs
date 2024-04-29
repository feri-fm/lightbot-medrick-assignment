using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramLoader : MonoBehaviour
{
    public int executionCounter { get; private set; }
    public List<CommandState> executionStack = new();
    public bool isExecuting { get; private set; }

    public ProgramState program { get; private set; }

    public BaseManager manager => BaseManager.instance;

    public CommandState currentCommand { get; private set; }

    private float executionTime;
    private float nextExecutionTime;

    private void Update()
    {
        if (isExecuting)
        {
            executionTime += Time.deltaTime;
            if (executionTime > nextExecutionTime)
            {
                if (currentCommand != null)
                    currentCommand.ExecuteEnd();

                currentCommand = StepForward();
                if (currentCommand != null)
                {
                    nextExecutionTime = executionTime + currentCommand.executionTime;
                }
                else
                {
                    PauseExecution();
                }
            }
        }
    }

    public void Clear()
    {
        ClearExecution();
    }
    public void ClearExecution()
    {
        currentCommand = null;
        executionCounter = 0;
        executionTime = 0;
        nextExecutionTime = 0;
        isExecuting = false;
        executionStack.Clear();
    }

    public void LoadProgram(Program prefab)
    {
        Clear();
        program = prefab.CreateState();
    }

    public void InsertProcedure(string key)
    {
        var procedure = program.GetProcedure(key);
        InsertProcedure(procedure);
    }
    public void InsertProcedure(ProcedureState procedure)
    {
        executionStack.InsertRange(executionCounter, procedure.commands);
    }
    public CommandState StepForward()
    {
        if (executionCounter < executionStack.Count)
        {
            var command = executionStack[executionCounter];
            executionCounter++;
            command.Execute();
            return command;
        }
        return null;
    }
    public void StartExecution()
    {
        isExecuting = true;
    }
    public void PauseExecution()
    {
        isExecuting = false;
    }
    public void StopExecution()
    {
        ClearExecution();
    }
}
