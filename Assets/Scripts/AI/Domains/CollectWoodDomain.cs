using System;
using FluidHTN;

public static class CollectWoodDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Collect Wood Domain")
            .Select("Collect Wood")
                .Splice(PrimitiveActions.CollectWoodAction)
                .Sequence("Buy worker and collect wood")
                    .CanCollect("Gold")
                    .Splice(RecruitWorkerDomain.Create())
                    .Splice(PrimitiveActions.CollectWoodAction)
                .End()
                .Sequence("Re-assign worker to collect wood")
                    .CanCollect("Wood")
                    .UnassignWorker()
                    .Splice(PrimitiveActions.CollectWoodAction)
                .End()
            .End()
            .Build();
    }
}

