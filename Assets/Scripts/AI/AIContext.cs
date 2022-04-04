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
    AvailableWood,
    AvailableGold,
    AvailableFood,
    IdleWorkers,
    FreeBuildingSpots
}

public enum GoalState
{
    None,
    BuildBarracks
}

public class AIContext : BaseContext<int>
{
    public override List<string> MTRDebug { get; set; } = null;
    public override List<string> LastMTRDebug { get; set; } = null;
    public override bool DebugMTR { get; } = false;
    public override Queue<IBaseDecompositionLogEntry> DecompositionLog { get; set; } = null;
    public override bool LogDecomposition { get; } = false;

    public override IFactory Factory { get; set; } = new DefaultFactory();

    private int[] _worldState = new int[Enum.GetValues(typeof(AIWorldState)).Length];
    public override int[] WorldState => _worldState;

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

    public void SetState(AIWorldState state, int value, EffectType type = EffectType.PlanAndExecute)
    {
        SetState((int)state, value, true, type);
    }

    public int GetState(AIWorldState state)
    {
        return GetState(state);
    }

    public void AddResource(string resourceType, int amount)
    {
        int currentAmount;
        int newAmount;

        switch (resourceType)
        {
            case "Gold":
                currentAmount = GetState(AIWorldState.AvailableGold);
                newAmount = currentAmount + amount;
                SetState(AIWorldState.AvailableGold, newAmount);
                break;

            case "Wood":
                currentAmount = GetState(AIWorldState.AvailableWood);
                newAmount = currentAmount + amount;
                SetState(AIWorldState.AvailableWood, newAmount);
                break;

            case "Food":
                currentAmount = GetState(AIWorldState.AvailableFood);
                newAmount = currentAmount + amount;
                SetState(AIWorldState.AvailableFood, newAmount);
                break;

            default:
                throw new Exception($"Unexpected resource type {resourceType}");
        }
    }

    public bool HasResources(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "Gold":
                return GetState(AIWorldState.AvailableGold) >= amount;

            case "Wood":
                return GetState(AIWorldState.AvailableWood) >= amount;

            case "Food":
                return GetState(AIWorldState.AvailableFood) >= amount;

            default:
                throw new Exception($"Unexpected resource type {resourceType}");
        }
    }

    public void RemoveResource(string resourceType, int amount)
    {
        AddResource(resourceType, -amount);
    }

    public void MakeBuildingSpotFree()
    {
        int currentFreeBuildingSpots = GetState(AIWorldState.FreeBuildingSpots);
        SetState(AIWorldState.FreeBuildingSpots, currentFreeBuildingSpots+1);
    }

    public void MakeBuildingSpotOccupied()
    {
        int currentFreeBuildingSpots = GetState(AIWorldState.FreeBuildingSpots);
        SetState(AIWorldState.FreeBuildingSpots, currentFreeBuildingSpots-1);
    }

    public bool HasFreeBuildingSpot()
    {
        return GetState(AIWorldState.FreeBuildingSpots) > 0;
    }

    public void MakeWorkerIdle()
    {
        int currentIdleWorkers = GetState(AIWorldState.IdleWorkers);
        SetState(AIWorldState.IdleWorkers, currentIdleWorkers+1);
    }

    public void MakeWorkerBusy()
    {
        int currentIdleWorkers = GetState(AIWorldState.IdleWorkers);
        SetState(AIWorldState.IdleWorkers, currentIdleWorkers-1);
    }

    public bool HasIdleWorker()
    {
        return GetState(AIWorldState.IdleWorkers) > 0;
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
