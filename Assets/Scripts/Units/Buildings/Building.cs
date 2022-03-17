using System;
using UnityEngine;

public class Building : GameUnit
{
    public new BuildingData Data
    {
        get
        {
            return (BuildingData)base.Data;
        }
    }

    public Building(BuildingData data) : base(data)
    {
    }
}

