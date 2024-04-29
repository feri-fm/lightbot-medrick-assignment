using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : BasePanel
{
    public ListLoaderMember levels;

    public override void OnOpen()
    {
        base.OnOpen();
        levels.Setup(menu.config.levels);
    }
}
