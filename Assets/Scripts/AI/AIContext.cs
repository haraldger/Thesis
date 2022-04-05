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
    GoldCollectionRate,
    WoodCollectionRate,
    FoodCollectionRate,
    IdleWorkers,
    BusyWorkers,
    FreeBuildingSpots,
    OccupiedBuildingSpots
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
    public override bool DebugMTR { get; } = true;
    public override Queue<IBaseDecompositionLogEntry> DecompositionLog { get; set; } = null;
    public override bool LogDecomposition { get; } = true;

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

    public void SenseState(AIWorldState state, int value)
    {
        WorldState[(int)state] = value;
        IsDirty = true;
    }

    public void SetState(AIWorldState state, int value, EffectType type = EffectType.PlanAndExecute)
    {
        SetState((int)state, value, true, type);
    }

    public int GetState(AIWorldState state)
    {
        return GetState((int)state);
    }



    // Resources

    public void SenseResource(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "Gold":
                SenseState(AIWorldState.AvailableGold, amount);
                break;

            case "Wood":
                SenseState(AIWorldState.AvailableWood, amount);
                break;

            case "Food":
                SenseState(AIWorldState.AvailableFood, amount);
                break;

            default:
                throw new Exception($"Unexpected resource type {resourceType}");
        }
    }

    public void AddResource(string resourceType, int amount, EffectType type = EffectType.PlanAndExecute)
    {
        int currentAmount;
        int newAmount;

        switch (resourceType)
        {
            case "Gold":
                currentAmount = GetState(AIWorldState.AvailableGold);
                newAmount = currentAmount + amount;
                SetState(AIWorldState.AvailableGold, newAmount, type);
                break;

            case "Wood":
                currentAmount = GetState(AIWorldState.AvailableWood);
                newAmount = currentAmount + amount;
                SetState(AIWorldState.AvailableWood, newAmount, type);
                break;

            case "Food":
                currentAmount = GetState(AIWorldState.AvailableFood);
                newAmount = currentAmount + amount;
                SetState(AIWorldState.AvailableFood, newAmount, type);
                break;

            default:
                throw new Exception($"Unexpected resource type {resourceType}");
        }
    }

    public void RemoveResource(string resourceType, int amount, EffectType type = EffectType.PlanAndExecute)
    {
        AddResource(resourceType, -amount, type);
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


    // Collection rates
    // This keeps tracks of the expected incoming rate of resources, based
    // on number of workers assigned to a collection task

    public void AddCollector(string resourceType)
    {
        WorkerData data = (WorkerData)Array.Find(Globals.TROOP_DATA, data => data.code == "Worker");
        int collectionRate = data.collectionAmount / data.collectionSpeed;

        int currentTotalRate;
        switch (resourceType)
        {
            case "Gold":
                currentTotalRate = GetState(AIWorldState.GoldCollectionRate);
                SetState(AIWorldState.GoldCollectionRate, currentTotalRate + collectionRate);
                break;

            case "Wood":
                currentTotalRate = GetState(AIWorldState.WoodCollectionRate);
                SetState(AIWorldState.WoodCollectionRate, currentTotalRate + collectionRate);
                break;

            case "Food":
                currentTotalRate = GetState(AIWorldState.FoodCollectionRate);
                SetState(AIWorldState.FoodCollectionRate, currentTotalRate + collectionRate);
                break;

            default:
                throw new Exception($"Unexpected resource type {resourceType}");
        }
    }

    public void RemoveCollector(string resourceType)
    {
        WorkerData data = (WorkerData)Array.Find(Globals.TROOP_DATA, data => data.code == "Worker");
        int collectionRate = data.collectionAmount / data.collectionSpeed;

        int currentTotalRate;
        switch (resourceType)
        {
            case "Gold":
                currentTotalRate = GetState(AIWorldState.GoldCollectionRate);
                SetState(AIWorldState.GoldCollectionRate, currentTotalRate + collectionRate);
                break;

            case "Wood":
                currentTotalRate = GetState(AIWorldState.WoodCollectionRate);
                SetState(AIWorldState.WoodCollectionRate, currentTotalRate + collectionRate);
                break;

            case "Food":
                currentTotalRate = GetState(AIWorldState.FoodCollectionRate);
                SetState(AIWorldState.FoodCollectionRate, currentTotalRate + collectionRate);
                break;

            default:
                throw new Exception($"Unexpected resource type {resourceType}");
        }
    }

    public int GetCollectionRate(string resourceType)
    {
        switch (resourceType)
        {
            case "Gold":
                return GetState(AIWorldState.GoldCollectionRate);

            case "Wood":
                return GetState(AIWorldState.WoodCollectionRate);

            case "Food":
                return GetState(AIWorldState.FoodCollectionRate);

            default:
                throw new Exception($"Unexpected resource type {resourceType}");
        }
    }



    // Building Spots

    public void MakeBuildingSpotFree(EffectType type = EffectType.PlanAndExecute)
    {
        int currentFreeBuildingSpots = GetState(AIWorldState.FreeBuildingSpots);
        SetState(AIWorldState.FreeBuildingSpots, currentFreeBuildingSpots+1, type);
    }

    public void MakeBuildingSpotOccupied(EffectType type = EffectType.PlanAndExecute)
    {
        int currentFreeBuildingSpots = GetState(AIWorldState.FreeBuildingSpots);
        SetState(AIWorldState.FreeBuildingSpots, currentFreeBuildingSpots-1, type);
    }

    public bool HasFreeBuildingSpot()
    {
        return GetState(AIWorldState.FreeBuildingSpots) > 0;
    }



    // Workers

    public void MakeWorkerIdle(EffectType type = EffectType.PlanAndExecute)
    {
        int currentIdleWorkers = GetState(AIWorldState.IdleWorkers);
        SetState(AIWorldState.IdleWorkers, currentIdleWorkers+1, type);
    }

    public void MakeWorkerBusy(EffectType type = EffectType.PlanAndExecute)
    {
        int currentIdleWorkers = GetState(AIWorldState.IdleWorkers);
        SetState(AIWorldState.IdleWorkers, currentIdleWorkers-1, type);
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
