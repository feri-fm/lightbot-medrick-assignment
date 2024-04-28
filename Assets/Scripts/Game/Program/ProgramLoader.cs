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

    private float executionTime;
    private float nextExecutionTime;

    private void Update()
    {
        if (isExecuting)
        {
            executionTime += Time.deltaTime;
            if (executionTime > nextExecutionTime)
            {
                var command = StepForward();
                if (command != null)
                {
                    nextExecutionTime = executionTime + command.executionTime;
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
        executionCounter = 0;
        executionTime = 0;
        isExecuting = false;
        executionStack.Clear();
    }

    public void LoadProgram(Program prefab)
    {
        Clear();
        program = prefab.CreateState();
    }

    public void QueueProcedure(string key)
    {
        var procedure = program.GetProcedure(key);
        executionStack.AddRange(procedure.commands);
    }
    public CommandState StepForward()
    {
        if (executionCounter > executionStack.Count)
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
        Clear();
    }
}
