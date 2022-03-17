using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    public static TroopManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecruitTroop(TroopData data, Vector3 position)
    {
        Troop troop = new Troop(data);
        RecruitTroop(troop, position);
    }

    public void RecruitTroop(Troop troop, Vector3 position)
    { 
        // Check resource constraints
        bool hasResources = true;
        foreach (CostValue cost in troop.Data.costs)
        {
            if (!Globals.RESOURCE_DATA[cost.code].CanConsumeResource(cost.value))
            {
                hasResources = false;
                break;
            }
        }

        // Check building constraints
        bool hasRequiredBuildings = false;
        foreach (Building building in Globals.CURRENT_BUILDINGS)
        {
            if (building.Data.recruitingOptions.Contains(troop.Data))
            {
                hasRequiredBuildings = true;
                break;
            }
        }

        if (hasResources && hasRequiredBuildings)
        {
            troop.InstantiatePrefab(position);
            foreach (CostValue cost in troop.Data.costs)
                Globals.RESOURCE_DATA[cost.code].ConsumeResource(cost.value);
        }
    }
}
