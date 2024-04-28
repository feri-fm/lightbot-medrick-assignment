using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureCommand : Command
{
    public override CommandState CreateState() => new ProcedureCommandState();
}
public class ProcedureCommandState : CommandState
{

}