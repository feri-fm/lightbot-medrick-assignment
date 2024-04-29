using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureListItem : ListItem<ProcedureState>
{
    public TextMember key;
    public TextMember commandsCount;
    public ListLoaderMember commands;
    public GameObjectMember selected;
    public GameObjectMember emptyCommands;

    public GameManager game => GameManager.instance;

    public override void Setup()
    {
        base.Setup();
        key.text = data.key;
        commandsCount.text = $"{data.commands.Count}/{data.maxCommands}";
        commands.Setup(data.commands);
        emptyCommands.SetActive(data.commands.Count == 0);
        selected.SetActive(game.selectedProcedure == data);
    }

    [Member]
    public void Select()
    {
        game.SelectProcedure(data);
    }
}
