using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public override CommandState CreateState() => new MoveCommandState();
}
public class MoveCommandState : CommandState
{

}