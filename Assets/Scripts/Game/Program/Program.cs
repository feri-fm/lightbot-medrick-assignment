using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Program : MonoBehaviour
{
    public List<Procedure> procedures = new();

    public ProgramState CreateState()
    {
        var state = new ProgramState();
        state.Setup(this);
        return state;
    }
}
public class ProgramState
{
    public Program prefab { get; private set; }
    public List<ProcedureState> procedures { get; } = new();

    public void Setup(Program prefab)
    {
        this.prefab = prefab;
        foreach (var procedurePrefab in prefab.procedures)
        {
            var procedureState = procedurePrefab.CreateState();
            procedures.Add(procedureState);
        }
    }

    public ProcedureState GetProcedure(string key)
    {
        return procedures.FirstOrDefault(e => e.key == key);
    }
}
