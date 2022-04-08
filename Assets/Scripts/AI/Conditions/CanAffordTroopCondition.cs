using System;
using FluidHTN;
using FluidHTN.Conditions;

public class CanAffordTroopCondition : ICondition<int>
{
    public string Name { get; }

    public string TroopType { get; }

    public CanAffordTroopCondition(string troopType)
    {
        Name = $"Can afford {troopType}";
        TroopType = troopType;
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            // Check resource constraints
            var costs = Array.Find(Globals.TROOP_DATA, data => data.code == TroopType).costs;
            foreach (var cost in costs)
            {
                if (!context.HasResources(cost.code, cost.value))
                {
                    if (context.LogDecomposition) context.Log(Name, $"Not enough resource {cost.code}", context.CurrentDecompositionDepth + 1, this);
                    return false;
                }
            }

            if (context.LogDecomposition) context.Log(Name, $"CanAffordTroopCondition.IsValid({TroopType}:true)", context.CurrentDecompositionDepth + 1, this);
            return true;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

