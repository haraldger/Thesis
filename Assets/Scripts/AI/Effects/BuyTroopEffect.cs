using System;
using FluidHTN;
using FluidHTN.Effects;

public class BuyTroopEffect : IEffect<int>
{
    public string Name { get; }

    public string TroopType { get; }

    public EffectType Type { get; }

    public BuyTroopEffect(string troopType, EffectType type = EffectType.PlanAndExecute)
    {
        Name = $"Buy troop {troopType}";
        TroopType = troopType;
        Type = type;
    }

    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            GameUnitData unit = Array.Find(Globals.TROOP_DATA, data => data.code == TroopType);
            var costs = unit.costs;
            foreach (var cost in costs)
            {
                context.RemoveResource(cost.code, cost.value, Type);
            }

            if (context.LogDecomposition) context.Log(Name, $"BuyTroopEffect.Apply({TroopType})", context.CurrentDecompositionDepth + 1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

