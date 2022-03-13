using System;
using System.Collections.Generic;

public class TroopData : GameUnitData
{
    public TroopData(string code, int hp, params CostValue[] costs) : base(code, hp, costs)
    {
    }
}

