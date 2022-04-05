using System;
using FluidHTN;

public static class RecruitWorkerDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Recruit Worker Domain")
            .Select("Recruit worker")
                .Splice(PrimitiveActions.RecruitWorkerAction)
                .Sequence("Build citadel and recruit worker")
                    .CanAffordTroop("Worker")
                    .Splice(BuildCitadelDomain.Create())
                    .Splice(PrimitiveActions.RecruitWorkerAction)
                .End()
                .Sequence("Wait for food and recruit worker")
                    .CanRecruit("Worker")
                    .WaitForResources("Worker")
                    .Splice(PrimitiveActions.RecruitWorkerAction)
                .End()
                .Sequence("Collect food and recruit worker")
                    .CanRecruit("Worker")
                    .Splice(CollectFoodDomain.Create()) // Collect food
                    .WaitForResources("Worker")
                    .Splice(PrimitiveActions.RecruitWorkerAction)
                .End()
                .Sequence("Build citadel, wait for resources and recruit worker")
                    .IsCollecting("Food")
                    .Splice(BuildCitadelDomain.Create()) // Build citadel
                    .WaitForResources("Worker")
                    .Splice(PrimitiveActions.RecruitWorkerAction)
                .End()
            .End()
            .Build();
    }
}

