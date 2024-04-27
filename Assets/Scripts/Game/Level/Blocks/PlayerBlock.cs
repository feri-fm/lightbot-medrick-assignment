using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : Block<PlayerBlockState>
{
    public override BlockState CreateState() => new PlayerBlockState();
}
public class PlayerBlockState : BlockState
{

}
