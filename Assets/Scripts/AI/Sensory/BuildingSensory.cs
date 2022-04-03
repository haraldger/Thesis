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

    /// <summary>
    /// This method sets the world state permanently. That means that once set,
    /// the effects will not be removed. As such, this method should not be used
    /// during planning, but to update the world state externally. It's intended
    /// to facilitate the passing of time, where in-game event may alter the
    /// world state.
    /// </summary>
    public void Tick()
    {
        SetCanBuildBarracks();
        SetCanBuildFarm();
        SetCanBuildCitadel();
    }

    /// <summary>
    /// This method updates the world state during planning. The changes to the
    /// world state are marked with the "PlanAndExecute" effect type, and will be
    /// undone during execution of a plan. Do not use this method to update the
    /// world state externally, i.e. outside of planning.
    /// </summary>
    public void UpdateWorldState()
    {
        UpdateCanBuild("Barracks");
        UpdateCanBuild("Farm");
        UpdateCanBuild("Citadel");
    }

    //--------------------------------------- Internal routines

    private void SetCanBuildBarracks()
    {
        _context.SetState(AIWorldState.CanBuildBarracks, CanBuildBuilding("Barracks"), EffectType.Permanent);
    }

    private void SetCanBuildFarm()
    {
        _context.SetState(AIWorldState.CanBuildFarm, CanBuildBuilding("Farm"), EffectType.Permanent);
    }

    private void SetCanBuildCitadel()
    {
        _context.SetState(AIWorldState.CanBuildCitadel, CanBuildBuilding("Citadel"), EffectType.Permanent);
    }

    private bool FreeBuildingSpot()
    {
        return AIManager.Instance.GetFreeBuildingSpot() != null;
    }


    //--------------------------------------- Public sensory getters and setters

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
            if (_resourceData[cost.code].CanConsumeResource(cost.value)) return false;
        }

        return true;
    }

    public void UpdateCanBuild(string buildingType)
    {
        if (CanBuildBuilding(buildingType))
        {
            switch (buildingType)
            {
                case "Barracks":
                    _context.SetState(AIWorldState.CanBuildBarracks, true, EffectType.PlanAndExecute);
                    break;

                case "Farm":
                    _context.SetState(AIWorldState.CanBuildFarm, true, EffectType.PlanAndExecute);
                    break;

                case "Citadel":
                    _context.SetState(AIWorldState.CanBuildCitadel, true, EffectType.PlanAndExecute);
                    break;

                default:
                    break;
            }
        }

    }


    //--------------------------------------- Private Fields

    private readonly AIContext _context;
    private readonly BuildingData[] _buildingData;
    private readonly IDictionary<string, GameResourceData> _resourceData;
}

