using System;
using FluidHTN;
using FluidHTN.Effects;

public class MakeBuildingSpotFreeEffect : IEffect<int>
{
    public string Name { get; }

    public EffectType Type { get; }

    public MakeBuildingSpotFreeEffect(EffectType type = EffectType.PlanAndExecute)
    {
        Name = "Make building spot free";
        Type = type;
    }

    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            var currentValue = context.GetState(AIWorldState.FreeBuildingSpots);
            context.MakeBuildingSpotFree(Type);
            if (context.LogDecomposition) context.Log(Name, $"MakeBuildingSpotFreeEffect.Apply({currentValue}+1,{Type})", context.CurrentDecompositionDepth + 1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

