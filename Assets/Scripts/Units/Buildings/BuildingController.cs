using System;
using UnityEngine;

public class BuildingController : UnitController
{
    public BuildingData data;

    public Vector3 RallyPoint
    {
        get
        {
            Transform rallyPointComponent = gameObject.transform.Find("RallyPoint");
            if (rallyPointComponent == null) return gameObject.transform.position;

            return rallyPointComponent.position;
        }
        set
        {
            Transform rallyPointComponent = gameObject.transform.Find("RallyPoint");
            if(rallyPointComponent != null) rallyPointComponent.position = value;
        }
    }

    public Vector3 SpawnPoint
    {
        get
        {
            Transform spawnPointComponent = gameObject.transform.Find("SpawnPoint");
            if (spawnPointComponent == null) return gameObject.transform.position;

            return spawnPointComponent.position;
        }
    }



    public override void Awake()
    {
        base.Awake();

        RallyPoint = SpawnPoint;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }


    // TODO: Refactor game actions to return bool or TaskStatus
    // Do in all classes
    // Add unit tests and integration tests
    public void RecruitTroop(TroopData troopData)
    {
        if (data.recruitingOptions.Contains(troopData))
        {
            TroopManager.Instance.RecruitTroop(troopData, SpawnPoint, RallyPoint);
        }
    }
}

