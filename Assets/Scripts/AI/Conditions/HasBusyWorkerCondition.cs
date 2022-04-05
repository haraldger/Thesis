using System;
using FluidHTN;
using FluidHTN.Conditions;

public class HasBusyWorkerCondition : ICondition<int>
{
    public string Name { get; }

    public HasBusyWorkerCondition()
    {
        Name = "Has busy worker";
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            bool result = context.GetState(AIWorldState.BusyWorkers) > 0;
            if (context.LogDecomposition) context.Log(Name, $"HasBusyWorkerCondition.IsValid:{result}", context.CurrentDecompositionDepth+1, this);
            return result;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

