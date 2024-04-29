using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class LevelLoader : MonoBehaviour
{
    public Transform container;

    public Level level { get; private set; }
    public List<Block> blocks { get; } = new();
    public List<BlockState> states { get; } = new();

    public LevelGround ground => level.ground;

    public BaseManager manager => BaseManager.instance;

    public ObjectPool pool { get; private set; }

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    public void Clear()
    {
        ClearBlocks();
        ClearStates();
    }
    public void ClearStates()
    {
        foreach (var state in states)
        {
            state.Remove();
        }
        states.Clear();
    }
    public void ClearBlocks()
    {
        foreach (var block in blocks)
        {
            block.Remove();
            block.Pool();
        }
        blocks.Clear();
    }

    public void LoadLevel(Level level)
    {
        Clear();
        this.level = level;
        foreach (var block in level.blocks)
        {
            CreateBlockState(block);
        }
    }
    public void LoadBlocks()
    {
        ClearBlocks();
        foreach (var state in states)
        {
            CreateBlock(state);
        }
    }

    public void ReloadLevel()
    {
        LoadLevel(level);
    }

    public BlockState CreateBlockState(Block prefab)
    {
        var state = prefab.CreateState();
        state.Setup(this, prefab);
        states.Add(state);
        return state;
    }
    public void RemoveBlockState(BlockState state)
    {
        state.Remove();
        states.Remove(state);
    }

    public Block CreateBlock(BlockState state)
    {
        var prefab = manager.config.GetBlock(state.key);
        var block = pool.Spawn(prefab, container);
        block.Setup(this, state);
        blocks.Add(block);
        return block;
    }
    public void RemoveBlock(Block block)
    {
        block.Remove();
        block.Pool();
        blocks.Remove(block);
    }

    public T GetBlockState<T>() where T : BlockState
    {
        return states.Find(e => e is T) as T;
    }
    public T GetBlockStateAt<T>(Vector3Int position) where T : BlockState
    {
        return states.Find(e => e is T && e.position == position) as T;
    }
    public T GetBlockStateAtTop<T>(Vector3Int position) where T : BlockState
    {
        var targets = states.Where(e => e is T && e.position.x == position.x && e.position.z == position.z).ToList();
        if (targets.Count > 0)
        {
            targets.Sort((a, b) => b.position.y.CompareTo(a.position.y));
            return targets[0] as T;
        }
        return null;
    }
}
