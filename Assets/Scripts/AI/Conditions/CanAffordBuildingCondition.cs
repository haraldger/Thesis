using System;
using FluidHTN;
using FluidHTN.Conditions;

public class CanAffordBuildingCondition : ICondition<int>
{
    public string Name { get; }

    public string BuildingType { get; }

    public CanAffordBuildingCondition(string buildingType)
    {
        Name = $"Can afford {buildingType}";
        BuildingType = buildingType;
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            // Resource constraints
            var costs = Array.Find(Globals.BUILDING_DATA, data => data.code == BuildingType).costs;

            if (costs == null)
            {
                if (context.LogDecomposition) context.Log(Name, $"Costs uninitialised", context.CurrentDecompositionDepth + 1, this);
                return false;

            }

            foreach (var cost in costs)
            {
                if (!context.HasResources(cost.code, cost.value))
                {
                    if (context.LogDecomposition) context.Log(Name, $"Not enough resources: {cost.code}", context.CurrentDecompositionDepth + 1, this);
                    return false;
                }
            }

            if (context.LogDecomposition) context.Log(Name, $"CanAffordBuildingCondition.IsValid({BuildingType}:true)", context.CurrentDecompositionDepth + 1, this);
            return true;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

