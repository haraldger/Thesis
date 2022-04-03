using System;
using System.Collections.Generic;
using System.Linq;
using FluidHTN;
using UnityEngine;

public class RecruitingSensory : ISensory
{

    public RecruitingSensory(AIContext context)
    {
        this._context = context;
        _troopData = Globals.TROOP_DATA;
        _buildingData = Globals.BUILDING_DATA;
        _resourceData = Globals.RESOURCE_DATA;
    }

    /// <summary>
    /// This method sets the world state permanently. That means that once set,
    /// the effects will not be removed. As such, this method should not be used
    /// during planning, but to update the world state externally. It's intended
    /// to facilitate the passing of time, where in-game event may alter the
    /// world state.
    /// </summary>
    public void Tick()
    {
        SetCanRecruitSwordsman();
        SetCanRecruitRanger();
        SetCanRecruitWorker();
    }

    /// <summary>
    /// This method updates the world state during planning. The changes to the
    /// world state are marked with the "PlanAndExecute" effect type, and will be
    /// undone during execution of a plan. Do not use this method to update the
    /// world state externally, i.e. outside of planning.
    /// </summary>
    public void UpdateWorldState()
    {
        UpdateCanRecruit("Swordsman");
        UpdateCanRecruit("Ranger");
        UpdateCanRecruit("Worker");
    }

    //--------------------------------------- Internal routines

    private void SetCanRecruitSwordsman()
    {
        _context.SetState(AIWorldState.CanRecruitSwordsman, CanRecruitTroop("Swordsman"), EffectType.Permanent);
    }

    private void SetCanRecruitRanger()
    {
        _context.SetState(AIWorldState.CanRecruitRanger, CanRecruitTroop("Ranger"), EffectType.Permanent);
    }

    private void SetCanRecruitWorker()
    {
        _context.SetState(AIWorldState.CanRecruitWorker, CanRecruitTroop("Worker"), EffectType.Permanent);
    }


    //--------------------------------------- Public sensory getters

    // Check if troop can be recruited
    public bool CanRecruitTroop(string troopType)
    {
        TroopData troop;
        try
        {
            troop = Array.Find(_troopData, data => data.code == troopType);
        }
        catch (ArgumentNullException)    // Troop type doesn't exist
        {
            return false;
        }

        // Check resource constraints
        foreach (var cost in troop.costs)
        {
            if (!_resourceData[cost.code].CanConsumeResource(cost.value))
            {
                return false;
            }
        }

        // Check building constraints
        string requiredBuilding;
        try
        {
            requiredBuilding = _buildingData.First(building => building.recruitingOptions.Where(availableTroop => availableTroop == troop).Any()).code;
        }
        catch (InvalidOperationException)
        {
            return false;
        }

        if (!_context.HasBuildingType(requiredBuilding))
        {
            return false;
        }


        return true;
    }

    public void UpdateCanRecruit(string troopType)
    {
        if (CanRecruitTroop(troopType))
        {
            switch (troopType)
            {
                case "Swordsman":
                    _context.SetState(AIWorldState.CanRecruitSwordsman, true, EffectType.PlanAndExecute);
                    break;

                case "Ranger":
                    _context.SetState(AIWorldState.CanRecruitRanger, true, EffectType.PlanAndExecute);
                    break;

                case "Worker":
                    _context.SetState(AIWorldState.CanRecruitWorker, true, EffectType.PlanAndExecute);
                    break;

                default:
                    break;
            }
        }
    }

    private readonly AIContext _context;
    private readonly TroopData[] _troopData;
    private readonly BuildingData[] _buildingData;
    private readonly IDictionary<string, GameResourceData> _resourceData;
}

