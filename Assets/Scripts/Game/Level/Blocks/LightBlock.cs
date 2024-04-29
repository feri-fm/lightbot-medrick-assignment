using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : Block<LightBlockState>
{
    public GameObject lightOn;
    public GameObject lightOff;

    public override BlockState CreateState() => new LightBlockState();

    protected override void Setup()
    {
        base.Setup();
        UpdateView();
    }

    public override void Update()
    {
        base.Update();
        UpdateView();
    }

    public void UpdateView()
    {
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
