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
        throw new NotImplementedException();
    }

    public TaskStatus Update(IContext<int> ctx)
    {
        throw new NotImplementedException();
    }
}

