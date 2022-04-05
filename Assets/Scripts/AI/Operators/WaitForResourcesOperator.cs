using System;
using FluidHTN;
using FluidHTN.Operators;

public class WaitForResourcesOperator : IOperator<int>
{
    public string ResourceType { get; }

    public int Amount { get; } 

    public WaitForResourcesOperator(string resourceType, int amount)
    {
        ResourceType = resourceType;
        Amount = amount;
    }

    public void Stop(IContext<int> ctx)
    {
    }

    public TaskStatus Update(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            AIWorldState state;
            switch (ResourceType)
            {
                case "Gold":
                    state = AIWorldState.AvailableGold;
                    break;

                case "Wood":
                    state = AIWorldState.AvailableWood;
                    break;

                case "Food":
                    state = AIWorldState.AvailableFood;
                    break;

                default:
                    throw new Exception($"Unexpected resource type {ResourceType}");
            }

            if (context.GetState(state) >= Amount)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Continue;
            }
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

