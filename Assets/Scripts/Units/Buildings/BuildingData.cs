using System;
using System.Collections.Generic;

public class BuildingData : GameUnitData
{
    public BuildingData(string code, int hp, IDictionary<GameResourceData, int> cost) : base(code, hp, cost)
    {
    }
}

