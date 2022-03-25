using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecruitingManager : MonoBehaviour
{
    public static RecruitingManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool RecruitTroop(TroopData troopData, Vector3 position, out TroopController newUnit)
    {
        return RecruitTroop(troopData, position, position, out newUnit);
    }

    public bool RecruitTroop(TroopData troopData, Vector3 position, Vector3 rallyPoint, out TroopController newUnit)
    {
        // Check resource constraints
        bool hasResources = true;
        foreach (CostValue cost in troopData.costs)
        {
            if (!Globals.RESOURCE_DATA[cost.code].CanConsumeResource(cost.value))
            {
                hasResources = false;
                break;
            }
        }

        // Check building constraints
        bool hasRequiredBuildings = false;
        foreach (BuildingController building in Globals.EXISTING_UNITS.Keys.Where(x => x is BuildingController))
        {
            if (building.data.recruitingOptions.Contains(troopData))
            {
                hasRequiredBuildings = true;
                break;
            }
        }

        if (hasResources && hasRequiredBuildings)
        {
            GameUnit troop = new GameUnit(troopData);
            troop.InstantiatePrefab(position);
            foreach (CostValue cost in troop.Data.costs)
                Globals.RESOURCE_DATA[cost.code].ConsumeResource(cost.value);
            var controller = troop.Instance.GetComponentInChildren<TroopController>();
            Globals.EXISTING_UNITS[controller] = troop;

            ((TroopController)troop.GetUnitController())?.MoveCommand(rallyPoint); // After spawning, move troop to rally point

            newUnit = controller;
            return true;
        }
        else
        {
            newUnit = null;
            return false;
        }
    }
}
