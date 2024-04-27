using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardCommand : Command
{
    public override CommandState CreateState() => new MoveForwardCommandState();
}
public class MoveForwardCommandState : CommandState
{

}