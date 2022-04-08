using System;
using FluidHTN;
using FluidHTN.Conditions;

public class IsCollectingCondition : ICondition<int>
{
    public string Name { get; }

    public string ResourceType { get; }

    public IsCollectingCondition(string resourceType)
    {
        Name = $"Is collecting {resourceType}";
        ResourceType = resourceType;
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            bool result = true;

            int collectionRate = context.GetCollectionRate(ResourceType);
            if(collectionRate <= 0)
            {
                result = false;
            }

            if (context.LogDecomposition) context.Log(Name, $"IsCollectingCondition.IsValid({ResourceType}):{result}", context.CurrentDecompositionDepth + 1, this);
            return result;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

