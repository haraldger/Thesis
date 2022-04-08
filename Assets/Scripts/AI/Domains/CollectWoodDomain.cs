using System;
using FluidHTN;

public static class CollectWoodDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Collect Wood Domain")
            .Select("Collect Wood")
                .CanCollect("Wood")

                .Splice(PrimitiveActions.CollectWoodAction)

                .Sequence("Recruit worker and collect wood")
                    .Splice(RecruitWorkerDomain.Create())   
                    .Splice(PrimitiveActions.CollectWoodAction)     
                .End()

                .Sequence("Re-assign worker to collect wood")
                    .UnassignWorker()
                    .Splice(PrimitiveActions.CollectWoodAction)
                .End()
            .End()
            .Build();
    }
}

