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
            // Check idle workers
            if (context.GetState(AIWorldState.IdleWorkers) <= 0)
            {
                if (context.LogDecomposition) context.Log(Name, $"No idle workers to collect {ResourceType}", context.CurrentDecompositionDepth + 1, this);
                return false;
            }

            // Check existing resource collection spot
            if (AIManager.Instance.GetResourceSpotType(ResourceType) == null)
            {
                if (context.LogDecomposition) context.Log(Name, $"No {ResourceType} to collect", context.CurrentDecompositionDepth + 1, this);
                return false;
            }

            if (context.LogDecomposition) context.Log(Name, $"CanCollectCondition.IsValid:true", context.CurrentDecompositionDepth + 1, this);
            return true;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

