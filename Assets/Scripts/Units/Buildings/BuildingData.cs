using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building", order = 1)]
public class BuildingData : GameUnitData
{
    public List<TroopData> recruitingOptions;
}

