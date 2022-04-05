using System;
using FluidHTN;

public static class CollectFoodDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Collect Food Domain")
            .Select("Collect Food")
                .Splice(PrimitiveActions.CollectFoodAction)
                .Sequence("Recruit worker and collect food")
                    .CanCollect("Food")
                    .Splice(RecruitWorkerDomain.Create())
                    .Splice(PrimitiveActions.CollectFoodAction)
                .End()
                .Sequence("Re-assign worker to collect food")
                    .CanCollect("Food")
                    .UnassignWorker()
                    .Splice(PrimitiveActions.CollectFoodAction)
                .End()
            .End()
            .Build();
    }
}

