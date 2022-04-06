using System;
using System.Collections.Generic;
using UnityEngine;

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
        SetCanBuildBarracks();
        SetCanBuildFarm();
        SetCanBuildCitadel();
    }

    //--------------------------------------- Internal routines

    private void SetCanBuildBarracks()
    {
        _context.SetState(AIWorldState.CanBuildBarracks, CanBuildBuilding("Barracks"));
    }

    private void SetCanBuildFarm()
    {
        _context.SetState(AIWorldState.CanBuildFarm, CanBuildBuilding("Farm"));
    }

    private void SetCanBuildCitadel()
    {
        _context.SetState(AIWorldState.CanBuildCitadel, CanBuildBuilding("Citadel"));
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
            Debug.Log($"Couldn't find _buildingData");
            return false;
        }

        // Check for available building spot
        if (!FreeBuildingSpot())
        {
            Debug.Log("No free building spot");
            return false;
        }

        // Check resource constraints
        foreach (var cost in building.costs)
        {
            if (!_resourceData[cost.code].CanConsumeResource(cost.value))
            {
                return false;
            } 

        }

        return true;
    }


    //--------------------------------------- Private Fields

    private readonly AIContext _context;
    private readonly BuildingData[] _buildingData;
    private readonly IDictionary<string, GameResourceData> _resourceData;
}

