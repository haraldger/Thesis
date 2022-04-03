using System;
using FluidHTN;
using FluidHTN.Effects;

public class UpdateWorldStateEffect : IEffect
{

    public string Name { get; }

    public EffectType Type => EffectType.PlanAndExecute;

    public UpdateWorldStateEffect()
    {
        Name = $"Update world state";
    }

    public void Apply(IContext ctx)
    {
        if (ctx is AIContext context)
        {
            AIManager.Instance.UpdateWorldState();
            if (context.LogDecomposition) context.Log(Name, $"UpdateWorldStateEffect.Apply", context.CurrentDecompositionDepth+1, this); 
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

