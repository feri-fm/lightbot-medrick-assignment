using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : Command
{
    public override CommandState CreateState() => new JumpCommandState();
}
public class JumpCommandState : CommandState
{

}
