using System;
using FluidHTN;
using FluidHTN.Effects;

public class AddNewWorkerEffect : IEffect<int>
{
    public string Name { get; }

    public EffectType Type { get; }

    public AddNewWorkerEffect(EffectType type = EffectType.PlanAndExecute)
    {
        Name = "Add new worker";
        Type = type;
    }


    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            context.AddNewWorker(Type);
            if (context.LogDecomposition) context.Log(Name, $"AddNewWorkerEffect.Apply:{Type}", context.CurrentDecompositionDepth + 1, this);
            return;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

