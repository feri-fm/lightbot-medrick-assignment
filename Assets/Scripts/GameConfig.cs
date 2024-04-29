using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public string menuScene = "Menu";
    public string gameScene = "Game";
    public Command[] commands;
    public Block[] blocks;
    public Level[] levels;

    public Command GetCommand(string key)
    {
        return commands.FirstOrDefault(e => e.key == key);
    }

    public Block GetBlock(string key)
    {
        return blocks.FirstOrDefault(e => e.key == key);
    }

    public Level GetLevel(string key)
    {
        return levels.FirstOrDefault(e => e.key == key);
    }

    public Level GetNextLevel(string key)
    {
        var index = -1;
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].key == key)
            {
                index = i;
                break;
            }
        }
        if (index == -1) return null;

        var nextIndex = index + 1;
        if (nextIndex >= levels.Length) return null;

        return levels[nextIndex];
    }
}
