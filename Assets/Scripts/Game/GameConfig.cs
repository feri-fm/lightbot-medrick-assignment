using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public Command[] commands;
    public Block[] blocks;
    public Level[] levels;

    public Block GetBlock(string key)
    {
        return blocks.First(e => e.key == key);
    }
}