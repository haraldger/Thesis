using System;
using System.Collections.Generic;
using FluidHTN;

public class BuildingSensory : ISensory
{
    public BuildingSensory(AIContext context)
    {
        this._context = context;
        _buildingData = Globals.BUILDING_DATA;
        _resourceData = Globals.RESOURCE_DATA;
    }

    public void Tick()
    {
        SenseFreeBuildingSpots();
        SenseFarms();
        SenseCitadels();
        SenseBarracks();
    }

    //--------------------------------------- Internal routines

    private void SenseFreeBuildingSpots()
    {
        int freeBuildingSpots = AIManager.Instance.GetFreeBuildingSpots().Count;
        _context.SenseState(AIWorldState.FreeBuildingSpots, freeBuildingSpots);
    }

    public void SenseFarms()
    {
        int farms = AIManager.Instance.GetBuildings("Farm").Count;
        _context.SenseState(AIWorldState.Farms, farms);
    }

    public void SenseBarracks()
    {
        int barracks = AIManager.Instance.GetBuildings("Barracks").Count;
        _context.SenseState(AIWorldState.Barracks, barracks);
    }

    public void SenseCitadels()
    {
        int citadels = AIManager.Instance.GetBuildings("Citadel").Count;
        _context.SenseState(AIWorldState.Citadels, citadels);
    }

    private bool FreeBuildingSpot()
    {
        return AIManager.Instance.GetFreeBuildingSpot() != null;
    }


    //--------------------------------------- Public sensory getters

    public bool CanBuildBuilding(string buildingType)
    {
        BuildingData building;
        try
        {
            building = Array.Find(_buildingData, data => data.code == buildingType);
        }
        catch (ArgumentNullException)
        {
            return false;
        }

        // Check for available building spot
        if (!FreeBuildingSpot()) return false;

        // Check resource constraints
        foreach (var cost in building.costs)
        {
            if (!_resourceData[cost.code].CanConsumeResource(cost.value)) return false;
        }

        return true;
    }


    //--------------------------------------- Private Fields

    private readonly AIContext _context;
    private readonly BuildingData[] _buildingData;
    private readonly IDictionary<string, GameResourceData> _resourceData;
}

