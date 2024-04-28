using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure : MonoBehaviour
{
    public string key => name;
    public int maxCommands = 1;

    public ProcedureState CreateState()
    {
        var state = new ProcedureState();
        state.Setup(this);
        return state;
    }
}
public class ProcedureState
{
    public Procedure prefab;
    public List<CommandState> commands { get; } = new();

    public string key => prefab.key;
    public int maxCommands => prefab.maxCommands;

    public void Setup(Procedure prefab)
    {
        this.prefab = prefab;
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
}