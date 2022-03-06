using System;
using System.Collections.Generic;

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
        // Check resource constraints
        var barracksData = _buildingData["Barracks"];
        var barracksCost = barracksData.Cost;
        foreach (var cost in barracksCost)
        {
            if (!cost.Key.CanConsumeResource(cost.Value))
            {
                _context.SetState(AIWorldState.CanBuildBarracks, false);
                return;
            } 
        }

        _context.SetState(AIWorldState.CanBuildBarracks, true);
    }

    private void SetCanBuildFarm()
    {
        // Check resource constraints
        var farmData = _buildingData["Farm"];
        var farmCost = farmData.Cost;
        foreach (var cost in farmCost)
        {
            if (!cost.Key.CanConsumeResource(cost.Value))
            {
                _context.SetState(AIWorldState.CanBuildFarm, false);
                return;
            }
        }

        _context.SetState(AIWorldState.CanBuildFarm, true);
    }

    private readonly AIContext _context;
    private readonly IDictionary<string, BuildingData> _buildingData;
}

