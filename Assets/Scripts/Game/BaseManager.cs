using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public GameConfig config;
    public PanelGroup panelGroup;

    public static BaseManager instance { get; private set; }

    public virtual void Awake()
    {
        instance = this;
        foreach (var command in config.commands)
        {
            command._Setup();
        }
    }

    public void MarkDirty()
    {
        foreach (var panel in panelGroup.panels)
        {
            if (panel is BasePanel basePanel)
            {
                basePanel.MarkDirty();
            }
        }
    }
}
