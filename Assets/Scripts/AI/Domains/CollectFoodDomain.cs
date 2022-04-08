using System;
using FluidHTN;

public static class CollectFoodDomain
{
    public static Domain<AIContext, int> Create()
    {
        var collectFoodDomain = new AIDomainBuilder("Collect Food Sub-Domain")
            .Select("Collect Food")
                .CanCollect("Food")

                .Splice(PrimitiveActions.CollectFoodAction)

                .Sequence("Recruit worker and collect food")
                    .Splice(RecruitWorkerDomain.Create())
                    .Splice(PrimitiveActions.CollectFoodAction)
                .End()

                .Sequence("Re-assign worker to collect food")
                    .UnassignWorker()
                    .Splice(PrimitiveActions.CollectFoodAction)
                .End()
            .End()
            .Build();

        return new AIDomainBuilder("Collect Food Domain")
            .Splice(collectFoodDomain)
            .Sequence("Build farm and collect")
                .Splice(BuildFarmDomain.Create())
                .Splice(collectFoodDomain)
            .End()
            .Build();
    }
}

