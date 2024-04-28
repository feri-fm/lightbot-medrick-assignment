using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStateListItem : ListItem<CommandState>
{
    public GameManager game => GameManager.instance;
    public ProgramLoader programLoader => game.programLoader;

    private PoolObject view;

    public override void Setup()
    {
        base.Setup();
        view = data.prefab.SpawnView(pool, transform);
    }
    public override void OnRemoved()
    {
        base.OnRemoved();
        view.Pool();
    }

    [Member]
    public void Remove()
    {
        game.RemoveCommand(data);
    }
}
