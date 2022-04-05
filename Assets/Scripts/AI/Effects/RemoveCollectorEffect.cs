using System;
using FluidHTN;
using FluidHTN.Effects;

public class RemoveCollectorEffect : IEffect<int>
{
    public string Name { get; }

    public EffectType Type { get; }

    public string ResourceType { get; }

    public RemoveCollectorEffect(string resourceType, EffectType type = EffectType.PlanAndExecute)
    {
        Name = $"Remove collector from {resourceType}";
        Type = type;
        ResourceType = resourceType;
    }

    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            context.RemoveCollector(ResourceType);
            if (context.LogDecomposition) context.Log(Name, $"RemoveCollectorEffect.Apply({ResourceType}:{Type})", context.CurrentDecompositionDepth + 1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

