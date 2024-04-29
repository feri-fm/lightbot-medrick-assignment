using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : Block<LightBlockState>
{
    public GameObject lightOn;
    public GameObject lightOff;

    public override BlockState CreateState() => new LightBlockState();

    public override void Update()
    {
        base.Update();
        lightOn.SetActive(state.isOn);
        lightOff.SetActive(!state.isOn);
    }
}
public class LightBlockState : BlockState
{
    public bool isOn;

    public void ToggleLight()
    {
        isOn = !isOn;
    }
}
