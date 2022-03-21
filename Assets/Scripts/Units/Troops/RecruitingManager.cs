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


    public void RecruitTroop(TroopData troopData, Vector3 position)
    {
        RecruitTroop(troopData, position, position);
    }

    public void RecruitTroop(TroopData troopData, Vector3 position, Vector3 rallyPoint)
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
            Globals.EXISTING_UNITS[troop.Instance.GetComponentInChildren<SoldierController>()] = troop;

            ((SoldierController)troop.GetUnitController())?.MoveCommand(rallyPoint); // After spawning, move troop to rally point
        }
    }
}
