using System;
using FluidHTN;
using FluidHTN.Effects;

public class MakeWorkerIdleEffect : IEffect<int>
{
    public string Name { get; }

    public EffectType Type { get; }

    public MakeWorkerIdleEffect(EffectType type = EffectType.PlanAndExecute)
    {
        Name = $"Make worker idle";
        Type = type;
    }

    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            var currentValue = context.GetState(AIWorldState.IdleWorkers);
            context.MakeWorkerIdle(Type);
            if (context.LogDecomposition) context.Log(Name, $"MakeWorkerIdleEffect.Apply({currentValue}+1:{Type})", context.CurrentDecompositionDepth+1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

