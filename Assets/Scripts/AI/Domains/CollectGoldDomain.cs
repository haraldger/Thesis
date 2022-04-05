using System;
using FluidHTN;

public static class CollectGoldDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Collect Gold Domain")
            .Select("Collect Gold")
                .Splice(PrimitiveActions.CollectGoldAction)
                .Sequence("Buy worker and collect gold")
                    .CanCollect("Gold")
                    .Splice(RecruitWorkerDomain.Create())
                    .Splice(PrimitiveActions.CollectGoldAction)
                .End()
                .Sequence("Re-assign worker to collect gold")
                    .CanCollect("Gold")
                    .UnassignWorker()
                    .Splice(PrimitiveActions.CollectGoldAction)
                .End()
            .End()
            .Build();
    }

}

