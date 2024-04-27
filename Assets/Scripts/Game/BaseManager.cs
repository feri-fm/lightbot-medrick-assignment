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
    }
}
