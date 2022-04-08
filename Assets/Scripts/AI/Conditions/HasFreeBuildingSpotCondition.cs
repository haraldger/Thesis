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
            bool result = true;

            // Check free building spot
            if (!context.HasFreeBuildingSpot())
            {
                result = false;
            }

            if (context.LogDecomposition) context.Log(Name, $"HasFreeBuildingSpotCondition.IsValid:{result}", context.CurrentDecompositionDepth + 1, this);
            return result;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

