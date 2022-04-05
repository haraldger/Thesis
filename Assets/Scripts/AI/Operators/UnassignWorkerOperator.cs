using System;
using FluidHTN;
using FluidHTN.Operators;

public class UnassignWorkerOperator : IOperator<int>
{
    public UnassignWorkerOperator()
    {
    }

    public void Stop(IContext<int> ctx)
    {
    }

    public TaskStatus Update(IContext<int> ctx)
    {
        if (ctx is AIContext)
        {
            return AIManager.Instance.UnassignWorker();
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

