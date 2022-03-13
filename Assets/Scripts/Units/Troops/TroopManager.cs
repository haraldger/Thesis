using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    public TroopManager Instance { get; private set; }

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
        bool canRecruit = false;

        // Check resource constraints
        foreach (CostValue cost in troop.Data.Costs)
        {
            if (Globals.RESOURCE_DATA[cost.Code].CanConsumeResource(cost.Value))
            {
                canRecruit = false;
                break;
            }
        }

        if (canRecruit)
        {
            troop.InstantiatePrefab(position);
            foreach (CostValue cost in troop.Data.Costs)
                Globals.RESOURCE_DATA[cost.Code].ConsumeResource(cost.Value);
        }
    }
}
