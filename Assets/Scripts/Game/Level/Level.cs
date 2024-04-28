using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string key => name;
    public LevelGround ground;
    public Program program;
    public Command[] commands;
    public Block[] blocks;
}
