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
        
    }

    //--------------------------------------- Internal routines

    


    //--------------------------------------- Public sensory getters

    // Check if troop can be recruited
    public bool CanRecruitTroop(string troopType)
    {
        TroopData troop;
        try
        {
            troop = Array.Find(_troopData, data => data.code == troopType);
        }
        catch (ArgumentNullException ex)    // Troop type doesn't exist
        {
            Debug.Log($"No troop type {troopType} found in global storage.");
            Debug.Log(ex.StackTrace);
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
        catch (InvalidOperationException ex)
        {
            Debug.Log($"Couldn't find required building for given troop type in global storage.");
            Debug.Log(ex.StackTrace);
            return false;
        }

        var existingUnits = Globals.EXISTING_UNITS.Keys;
        if (!existingUnits.Where(unit => unit.Data.code == requiredBuilding).Any()) return false;


        return true;
    }

    private readonly AIContext _context;
    private readonly TroopData[] _troopData;
    private readonly BuildingData[] _buildingData;
    private readonly IDictionary<string, GameResourceData> _resourceData;
}

