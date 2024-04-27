using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBlock : Block<PlatformBlockState>
{
    public override BlockState CreateState() => new PlatformBlockState();
}
public class PlatformBlockState : BlockState
{

}
