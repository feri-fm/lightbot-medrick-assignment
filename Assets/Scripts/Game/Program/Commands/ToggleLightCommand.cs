using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLightCommand : Command
{
    public override CommandState CreateState() => new ToggleLightCommandState();
}
public class ToggleLightCommandState : CommandState
{

}