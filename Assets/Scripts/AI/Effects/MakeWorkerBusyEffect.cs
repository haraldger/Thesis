using System;
using FluidHTN;
using FluidHTN.Effects;

public class MakeWorkerBusyEffect : IEffect<int>
{
    public string Name { get; }

    public EffectType Type { get; }

    public MakeWorkerBusyEffect(EffectType type = EffectType.PlanAndExecute)
    {
        Name = $"Make worker busy";
        Type = type;
    }

    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            var currentValue = context.GetState(AIWorldState.IdleWorkers);
            context.MakeWorkerBusy(Type);
            if (context.LogDecomposition) context.Log(Name, $"MakeWorkerBusyEffect.Apply({currentValue}-1:{Type})", context.CurrentDecompositionDepth + 1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

