using System;
using FluidHTN;
using FluidHTN.Conditions;

public class CanCollectCondition : ICondition
{
    public string Name { get; }

    public string ResourceType { get; }

    public CanCollectCondition(string resourceType)
    {
        Name = $"Can collect {resourceType}";
        ResourceType = resourceType;
    }

    public bool IsValid(IContext ctx)
    {
        if (ctx is AIContext context)
        {
            bool result = false;

            switch (ResourceType)
            {
                case "Gold":
                    result = context.HasState(AIWorldState.CanCollectGold);
                    break;

                case "Wood":
                    result = context.HasState(AIWorldState.CanCollectWood);
                    break;

                case "Food":
                    result = context.HasState(AIWorldState.CanCollectFood);
                    break;

                default:
                    break;
            }

            if (context.LogDecomposition) context.Log(Name, $"CanCollectCondition.IsValid({ResourceType}:{result})", context.CurrentDecompositionDepth + 1, this);
            return result;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

