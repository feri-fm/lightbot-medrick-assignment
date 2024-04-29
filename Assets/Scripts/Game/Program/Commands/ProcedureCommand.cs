using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureCommand : Command
{
    public string key;

    public override CommandState CreateState() => new ProcedureCommandState();
}
public class ProcedureCommandState : CommandState<ProcedureCommand>
{
    public override void Execute()
    {
        base.Execute();
        // var procedure = game.programLoader.program.GetProcedure(prefab.key);
        game.programLoader.InsertProcedure(prefab.key);
    }
}