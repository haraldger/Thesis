using System;
using UnityEngine;

public class BuildingController : UnitController
{
    public BuildingData data;

    public Vector3 RallyPoint
    {
        get
        {
            return gameObject.transform.Find("RallyPoint").transform.position;
        }
        set
        {
            RallyPoint = value;
        }
    }

    // TODO: Refactor game actions to return bool or TaskStatus
    // Do in all classes
    // Add unit tests and integration tests
    public void RecruitTroop(TroopData troopData)
    {
        if (data.recruitingOptions.Contains(troopData))
        {
            TroopManager.Instance.RecruitTroop(troopData, RallyPoint);
        }
    }
}

