using System;
using FluidHTN;
using FluidHTN.Effects;

public class AddCollectorEffect : IEffect<int>
{
    public string Name { get; }

    public string ResourceType { get; }

    public EffectType Type { get; }

    public AddCollectorEffect(string resourceType, EffectType type = EffectType.PlanAndExecute)
    {
        Name = $"Add collector to {resourceType}";
        ResourceType = resourceType;
        Type = type;
    }

    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            context.AddCollector(ResourceType);
            if (context.LogDecomposition) context.Log(Name, $"AddCollectorEffect.Apply({ResourceType}:{Type})", context.CurrentDecompositionDepth + 1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

