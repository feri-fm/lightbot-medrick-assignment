using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStateListItem : ListItem<CommandState>
{
    public GameObjectMember running;

    public GameManager game => GameManager.instance;
    public ProgramLoader programLoader => game.programLoader;

    private PoolObject view;

    public override void Setup()
    {
        base.Setup();
        view = data.prefab.SpawnView(pool, transform);
        running.SetActive(false);
    }
    public override void OnRemoved()
    {
        base.OnRemoved();
        view.Pool();
    }

    public override void Update()
    {
        base.Update();
        running.SetActive(programLoader.currentCommand == data);
    }

    [Member]
    public void Remove()
    {
        game.RemoveCommand(data);
    }
}
