using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureCommand : Command
{
    public string procedureKey;

    public override CommandState CreateState() => new ProcedureCommandState();
}
public class ProcedureCommandState : CommandState<ProcedureCommand>
{
    public override void Execute()
    {
        base.Execute();
        game.programLoader.InsertProcedure(prefab.procedureKey);
    }
}
