using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelListItem : ListItem<Level>
{
    public TextMember key;

    public override void Setup()
    {
        base.Setup();
        key.text = data.key;
    }

    [Member]
    public void Load()
    {
        MenuManager.instance.LoadLevel(data);
    }
}
