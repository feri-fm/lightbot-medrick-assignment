using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : BaseManager
{
    public MenuPanel menuPanel => panelGroup.GetPanel<MenuPanel>();

    public new static MenuManager instance { get; private set; }

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    private void Start()
    {
        menuPanel.OpenPanel();
    }

    public void LoadLevel(Level level)
    {
        SetSelectedLevel(level);
        LoadGameScene();
    }
}
