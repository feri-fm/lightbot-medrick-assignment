using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : Panel
{
    public BaseManager manager => BaseManager.instance;
    public MenuManager menu => MenuManager.instance;
    public GameManager game => GameManager.instance;

    private bool dirty;

    public override void OnOpen()
    {
        base.OnOpen();
        Render();
    }

    public void MarkDirty()
    {
        dirty = true;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        if (dirty)
        {
            dirty = false;
            Render();
        }
    }

    private void Render()
    {
        dirty = false;
        OnRender();
    }

    public virtual void OnRender() { }
}
