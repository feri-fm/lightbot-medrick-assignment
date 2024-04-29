using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Program : MonoBehaviour
{
    public List<Procedure> procedures = new();

    public ProgramState CreateState(ProgramLoader loader)
    {
        var state = new ProgramState();
        state.Setup(this, loader);
        return state;
    }
}
public class ProgramState
{
    public Program prefab { get; private set; }
    public ProgramLoader loader { get; private set; }
    public List<ProcedureState> procedures { get; } = new();

    public void Setup(Program prefab, ProgramLoader loader)
    {
        this.prefab = prefab;
        this.loader = loader;
        foreach (var procedurePrefab in prefab.procedures)
        {
            var procedureState = procedurePrefab.CreateState(this);
            procedures.Add(procedureState);
        }
    }

    public ProcedureState GetProcedure(string key)
    {
        return procedures.FirstOrDefault(e => e.key == key);
    }

    public void Clear()
    {
        foreach (var procedure in procedures)
        {
            procedure.Clear();
        }
    }

    public ProgramData Save()
    {
        return new ProgramData()
        {
            procedures = procedures.Select(e => e.Save()).ToArray(),
        };
    }
    public void Load(ProgramData data)
    {
        foreach (var procedure in procedures)
        {
            var procedureData = data.procedures.FirstOrDefault(e => e.key == procedure.key);
            if (procedureData != null)
            {
                procedure.Load(procedureData);
            }
        }
    }
}
public class ProgramData
{
    public ProcedureData[] procedures;
}
