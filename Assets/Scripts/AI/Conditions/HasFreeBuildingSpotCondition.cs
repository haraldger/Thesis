using System;
using FluidHTN;
using FluidHTN.Conditions;

public class HasFreeBuildingSpotCondition : ICondition<int>
{
    public string Name { get; }

    public HasFreeBuildingSpotCondition()
    {
        Name = $"Has free building spot";
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            // Check free building spot
            if (!context.HasFreeBuildingSpot())
            {
                if (context.LogDecomposition) context.Log(Name, $"No free building spot", context.CurrentDecompositionDepth + 1, this);
                return false;
            }

            if (context.LogDecomposition) context.Log(Name, $"HasFreeBuildingSpotCondition.IsValid:true", context.CurrentDecompositionDepth + 1, this);
            return true;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

