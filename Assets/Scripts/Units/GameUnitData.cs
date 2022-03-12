using System;
using System.Collections.Generic;

public class GameUnitData
{
    public string Code { get; private set; }

    public int HP { get; private set; }

    public IDictionary<GameResourceData, int> Cost { get; private set; }

    public GameUnitData(string code, int hp, IDictionary<GameResourceData, int> cost)
    {
        Code = code;
        HP = hp;
        Cost = cost;
    }
}

