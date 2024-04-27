using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : BaseManager
{
    public new static MenuManager instance { get; private set; }

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }
}
