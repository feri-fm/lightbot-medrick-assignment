using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseManager : MonoBehaviour
{
    public GameConfig config;
    public PanelGroup panelGroup;

    public static BaseManager instance { get; private set; }

    public const string SELECTED_LEVEL_KEY = "selected_level";

    public virtual void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;

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

    public void SetSelectedLevel(Level level)
    {
        PlayerPrefs.SetString(SELECTED_LEVEL_KEY, level.key);
    }
    public Level GetSelectedLevel()
    {
        var level = config.GetLevel(PlayerPrefs.GetString(SELECTED_LEVEL_KEY));
        return level;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadMenuScene() => LoadScene(config.menuScene);
    public void LoadGameScene() => LoadScene(config.gameScene);
}
