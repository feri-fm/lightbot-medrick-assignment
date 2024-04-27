using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunProcedureCommand : Command
{
    public override CommandState CreateState() => new RunProcedureCommandState();
}
public class RunProcedureCommandState : CommandState
{

}