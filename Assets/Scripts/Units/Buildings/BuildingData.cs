using System;
using System.Collections.Generic;

public class BuildingData : GameUnitData
{
    public BuildingData(string code, int hp, params CostValue[] costs) : base(code, hp, costs)
    {
    }
}

