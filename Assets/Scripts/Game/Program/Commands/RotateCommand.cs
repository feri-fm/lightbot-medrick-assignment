using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : Command
{
    public override CommandState CreateState() => new RotateCommandState();
}
public class RotateCommandState : CommandState
{

}