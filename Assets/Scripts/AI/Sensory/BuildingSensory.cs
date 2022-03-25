using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSensory : ISensory
{

    public BuildingSensory(AIContext context)
    {
        this._context = context;
        _buildingData = Globals.BUILDING_DATA;
    }

    public void Tick()
    {
        SetCanBuildBarracks();
        SetCanBuildFarm();
    }

    private void SetCanBuildBarracks()
    {
        // Check free building spot
        if (!FreeBuildingSpot())
        {
            _context.SetState(AIWorldState.CanBuildBarracks, false);
            return;
        }

        // Check resource constraints
        var barracksData = _buildingData.First(data => data.code == "Barracks");
        var barracksCosts = barracksData.costs;
        foreach (var cost in barracksCosts)
        {
            if (!Globals.RESOURCE_DATA[cost.code].CanConsumeResource(cost.value))
            {
                _context.SetState(AIWorldState.CanBuildBarracks, false);
                return;
            } 
        }

        _context.SetState(AIWorldState.CanBuildBarracks, true);
    }

    private void SetCanBuildFarm()
    {
        // Check free building spot
        if (!FreeBuildingSpot())
        {
            _context.SetState(AIWorldState.CanBuildFarm, false);
            return;
        }

        // Check resource constraints
        var farmData = _buildingData.First(data => data.code == "Farm");
        var farmCosts = farmData.costs;
        foreach (var cost in farmCosts)
        {
            if (!Globals.RESOURCE_DATA[cost.code].CanConsumeResource(cost.value))
            {
                _context.SetState(AIWorldState.CanBuildFarm, false);
                return;
            }
        }

        _context.SetState(AIWorldState.CanBuildFarm, true);
    }

    private bool FreeBuildingSpot()
    {
        return AIManager.Instance.GetFreeBuildingSpot() != null;
    }

    private readonly AIContext _context;
    private readonly BuildingData[] _buildingData;
}

