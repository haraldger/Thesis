using System;
public class Troop : GameUnit
{
    public new TroopData Data
    {
        get
        {
            return (TroopData)base.Data;
        }
    }

    public Troop(TroopData troopData): base(troopData)
    {
    }
}

