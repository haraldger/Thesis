using System;
using FluidHTN;
using FluidHTN.Conditions;

public class HasIdleWorkerCondition : ICondition<int>
{
    public string Name { get; }

    public HasIdleWorkerCondition()
    {
        Name = $"Has idle worker";
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            // Check idle workers
            if (context.GetState(AIWorldState.IdleWorkers) <= 0)
            {
                if (context.LogDecomposition) context.Log(Name, $"No idle workers", context.CurrentDecompositionDepth + 1, this);
                return false;
            }

            if (context.LogDecomposition) context.Log(Name, $"HasIdleWorkerCondition.IsValid:true", context.CurrentDecompositionDepth + 1, this);
            return true;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

