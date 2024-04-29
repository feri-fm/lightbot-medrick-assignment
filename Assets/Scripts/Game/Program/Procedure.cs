using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Procedure : MonoBehaviour
{
    public string key => name;
    public int maxCommands = 1;

    public ProcedureState CreateState(ProgramState program)
    {
        var state = new ProcedureState();
        state.Setup(this, program);
        return state;
    }
}
public class ProcedureState
{
    public Procedure prefab { get; private set; }
    public ProgramState program { get; private set; }
    public List<CommandState> commands { get; } = new();

    public string key => prefab.key;
    public int maxCommands => prefab.maxCommands;

    public void Setup(Procedure prefab, ProgramState program)
    {
        this.prefab = prefab;
        this.program = program;
    }

    public void Clear()
    {
        foreach (var command in commands)
            command.Remove();
        commands.Clear();
    }
    public CommandState AddCommand(Command prefab)
    {
        if (commands.Count >= maxCommands) return null;

        var command = prefab.CreateState();
        command.Setup(prefab, this);
        commands.Add(command);
        return command;
    }
    public void RemoveCommand(CommandState command)
    {
        command.Remove();
        commands.Remove(command);
    }

    public ProcedureData Save()
    {
        return new ProcedureData()
        {
            key = key,
            commands = commands.Select(e => e.Save()).ToArray(),
        };
    }
    public void Load(ProcedureData data)
    {
        Clear();
        foreach (var commandData in data.commands)
        {
            var command = program.loader.manager.config.GetCommand(commandData.key);
            if (command != null)
            {
                var commandState = AddCommand(command);
                commandState.Load(commandData);
            }
        }
    }
}
public class ProcedureData
{
    public string key;
    public CommandData[] commands;
}