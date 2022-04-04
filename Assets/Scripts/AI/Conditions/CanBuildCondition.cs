using System;
using System.Collections.Generic;
using FluidHTN;
using FluidHTN.Conditions;

public class CanBuildCondition : ICondition<int> 
{
    public string Name { get; }

    public string BuildingType { get; }

    public CanBuildCondition(string buildingType)
    {
        Name = $"Can build {buildingType}";
        BuildingType = buildingType;
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            // Check free building spot
            if (!context.HasFreeBuildingSpot())
            {
                if (context.LogDecomposition) context.Log(Name, $"No free building spot", context.CurrentDecompositionDepth+1, this);
                return false;
            }

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

            if (context.LogDecomposition) context.Log(Name, $"CanBuildCondition.IsValid:true", context.CurrentDecompositionDepth+1, this);
            return true;

        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

