using System;
using System.Collections;
using System.Collections.Generic;
using FluidHTN;
using FluidHTN.Contexts;
using FluidHTN.Debug;
using FluidHTN.Factory;

public enum AIWorldState
{
    CanBuildBarracks,
    CanBuildFarm,
    Goal
}

public enum GoalState
{
    None,
    BuildBarracks
}

public class AIContext : BaseContext
{
    public override List<string> MTRDebug { get; set; } = null;
    public override List<string> LastMTRDebug { get; set; } = null;
    public override bool DebugMTR { get; } = false;
    public override Queue<IBaseDecompositionLogEntry> DecompositionLog { get; set; } = null;
    public override bool LogDecomposition { get; } = false;

    public override IFactory Factory { get; set; } = new DefaultFactory();

    private byte[] _worldState = new byte[Enum.GetValues(typeof(AIWorldState)).Length];
    public override byte[] WorldState => _worldState;

    private IList<string> _buildings = new List<string>();

    public override void Init()
    {
        base.Init();
    }

    public bool HasGoal(GoalState goal)
    {
        return GetGoal() == goal;
    }

    public GoalState GetGoal()
    {
        return (GoalState)GetState(AIWorldState.Goal);
    }

    public void SetGoal(GoalState goal, bool setAsDirty = true, EffectType effectType = EffectType.PlanAndExecute)
    {
        SetState((int)AIWorldState.Goal, (byte)goal, setAsDirty, effectType);
    }

    public bool HasState(AIWorldState state, bool value)
    {
        return HasState((int)state, (byte)(value ? 1 : 0));
    }

    public bool HasState(AIWorldState state)
    {
        return HasState((int)state, 1);
    }

    public void SetState(AIWorldState state, bool value, EffectType type = EffectType.PlanAndExecute)
    {
        SetState((int)state, (byte)(value ? 1 : 0), true, type);
    }

    public byte GetState(AIWorldState state)
    {
        return GetState((int)state);
    }

    public void AddBuilding(string building)
    {
        _buildings.Add(building);
    }

    public void RemoveBuildin(string building)
    {
        _buildings.Remove(building);
    }

}
