using System;
using System.Collections.Generic;
using System.Linq;
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

    public void Tick()
    {
        SetCanRecruitSwordsman();
        SetCanRecruitRanger();
        SetCanRecruitWorker();
    }

    //--------------------------------------- Internal routines

    private void SetCanRecruitSwordsman()
    {
        _context.SetState(AIWorldState.CanRecruitSwordsman, CanRecruitTroop("Swordsman"));
    }

    private void SetCanRecruitRanger()
    {
        _context.SetState(AIWorldState.CanRecruitRanger, CanRecruitTroop("Ranger"));
    }

    private void SetCanRecruitWorker()
    {
        _context.SetState(AIWorldState.CanRecruitWorker, CanRecruitTroop("Worker"));
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

    private readonly AIContext _context;
    private readonly TroopData[] _troopData;
    private readonly BuildingData[] _buildingData;
    private readonly IDictionary<string, GameResourceData> _resourceData;
}

