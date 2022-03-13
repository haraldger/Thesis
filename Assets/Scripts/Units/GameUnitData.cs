using System;
using System.Collections.Generic;

public class GameUnitData
{
    public string Code { get; private set; }

    public int HP { get; private set; }

    public IList<CostValue> Costs { get; private set; }

    public GameUnitData(string code, int hp, params CostValue[] costs)
    {
        Code = code;
        HP = hp;

        Costs = new List<CostValue>();
        for(int i = 0; i < costs.Length; i++)
        {
            Costs.Add(costs[i]);
        }
    }
}

