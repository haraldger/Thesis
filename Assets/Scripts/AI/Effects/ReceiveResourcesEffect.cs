using System;
using FluidHTN;
using FluidHTN.Effects;

public class ReceiveResourcesEffect : IEffect<int>
{
    public string Name { get; }

    public string ResourceType { get; }

    public int Amount { get; }

    public EffectType Type { get; }

    public ReceiveResourcesEffect(string resourceType, int amount, EffectType type = EffectType.PlanAndExecute)
    {
        Name = $"Receive {amount} {resourceType}";
        ResourceType = resourceType;
        Amount = amount;
        Type = type; ;
    }


    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            context.AddResource(ResourceType, Amount, Type);
            if (context.LogDecomposition) context.Log(Name, $"ReceiveResourcesEffect.Apply({ResourceType}:{Amount}:{Type})", context.CurrentDecompositionDepth + 1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

