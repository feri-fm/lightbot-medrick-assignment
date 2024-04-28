using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPaletteListItem : ListItem<Command>
{
    public GameManager game => GameManager.instance;
    public ProgramLoader programLoader => game.programLoader;

    private PoolObject view;

    public override void Setup()
    {
        base.Setup();
        view = data.SpawnView(pool, transform);
    }
    public override void OnRemoved()
    {
        base.OnRemoved();
        view.Pool();
    }

    [Member]
    public void Add()
    {
        game.AddCommand(data);
    }
}
