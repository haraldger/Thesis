using System;
using FluidHTN;
using FluidHTN.Effects;

public class MakeBuildingSpotOccupiedEffect : IEffect<int>
{
    public string Name { get; }

    public EffectType Type { get; }

    public MakeBuildingSpotOccupiedEffect(EffectType type = EffectType.PlanAndExecute)
    {
        Name = $"Make building spot occupied";
        Type = type;
    }

    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            var currentValue = context.GetState(AIWorldState.FreeBuildingSpots);
            context.MakeBuildingSpotOccupied(Type);
            if (context.LogDecomposition) context.Log(Name, $"MakeBuildingSpotOccupiedEffect.Apply({currentValue}-1:{Type})", context.CurrentDecompositionDepth+1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

