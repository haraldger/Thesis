using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluidHTN;
using FluidHTN.Contexts;
using FluidHTN.Debug;
using FluidHTN.Factory;

public enum AIWorldState
{
    Goal,
    CanBuildBarracks,
    CanBuildFarm,
    CanBuildCitadel,
    CanRecruitSwordsman,
    CanRecruitRanger,
    CanRecruitWorker,
    CanCollectGold,
    CanCollectFood,
    CanCollectWood
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

    private IList<BuildingController> _buildings = new List<BuildingController>();
    private IList<SoldierController> _soldiers = new List<SoldierController>();
    private IList<WorkerController> _workers = new List<WorkerController>();

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

    public void SetState(AIWorldState state)
    {
        SetState(state, true);
    }

    public void SetState(AIWorldState state, bool value, EffectType type = EffectType.PlanAndExecute)
    {
        SetState((int)state, (byte)(value ? 1 : 0), true, type);
    }

    public byte GetState(AIWorldState state)
    {
        return GetState((int)state);
    }


    //---------------------------------------- Custom context extensions

    public void AddBuilding(BuildingController building)
    {
        _buildings.Add(building);
    }

    public void RemoveBuilding(BuildingController building)
    {
        _buildings.Remove(building);
    }

    public bool HasBuildingType(string buildingType)
    {
        return _buildings.Where(building => building.data.code == buildingType).Any();
    }

    // Returns any building of the specified type
    public BuildingController GetBuilding(string buildingType)
    {
        return _buildings.DefaultIfEmpty(null).FirstOrDefault(building => building.data.code == buildingType);
    }

    public void AddTroop(TroopController troop)
    {
        if (troop is SoldierController soldier)
        {
            _soldiers.Add(soldier);
        }
        else if (troop is WorkerController worker)
        {
            _workers.Add(worker);
        }
    }

    public void RemoveTroop(TroopController troop)
    {
        if (troop is SoldierController soldier)
        {
            _soldiers.Remove(soldier);
        }
        else if (troop is WorkerController worker)
        {
            _workers.Remove(worker);
        }
    }

    // Returns an idle worker
    public WorkerController GetIdleWorker()
    {
        return _workers.FirstOrDefault(worker => worker.CollectingTarget == null);
    }

}
