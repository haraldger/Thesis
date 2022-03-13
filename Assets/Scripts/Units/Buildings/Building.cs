using System;
using UnityEngine;

public class Building : GameUnit
{
    public Vector3 RallyPoint { get; set; }

    public Building(BuildingData data) : base(data)
    {
        var position = Instance.GetComponent<Transform>().position;
        RallyPoint = new Vector3(position.x + 10.0f, position.y, position.z);
    }
}

