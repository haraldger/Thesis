using System;
using FluidHTN;
using FluidHTN.Conditions;

public class CanCollectCondition : ICondition<int>
{
    public string Name { get; }

    public string ResourceType { get; }

    public CanCollectCondition(string resourceType)
    {
        Name = $"Can collect {resourceType}";
        ResourceType = resourceType;
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            bool result = true;

            // Check existing resource collection spot
            if (AIManager.Instance.GetResourceSpotType(ResourceType) == null)
            {
                result = false;
            }

            if (context.LogDecomposition) context.Log(Name, $"CanCollectCondition.IsValid({ResourceType}):{result}", context.CurrentDecompositionDepth + 1, this);
            return result;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

