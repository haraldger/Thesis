using System;
using FluidHTN;

public static class CollectFoodDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Collect Food Domain")
            .Select("Collect Food")

                .Splice(PrimitiveActions.CollectFoodAction)

                // Has available food source
                .Sequence("Recruit worker and collect food")
                    .CanCollect("Food")
                    .Splice(RecruitWorkerDomain.Create())
                    .Splice(PrimitiveActions.CollectFoodAction)
                .End()

                .Sequence("Re-assign worker to collect food")
                    .UnassignWorker()
                    .Splice(PrimitiveActions.CollectFoodAction)
                .End()

                // No available food source (farm)
                /// Splicing of farm domain here is okay, as it will not lead to infinite loop
                .Sequence("Build farm and collect food")

                    .Splice(BuildFarmDomain.Create())

                    .Select("Collect food")

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
                .End()
            .End()
            .Build();
    }
}

