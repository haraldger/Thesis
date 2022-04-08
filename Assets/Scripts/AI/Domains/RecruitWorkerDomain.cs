using System;
using FluidHTN;

public static class RecruitWorkerDomain
{
    public static Domain<AIContext, int> Create()
    {
        /// To avoid infinite loops during compilation of code (due
        /// to circular dependency), this class cannot call any other
        /// domains for splicing, apart from those in PrimitiveActions.
        /// As such, getting resources requires a custom implementation

        var getResourcesForWorker = new AIDomainBuilder("Get resources for worker")
            .Select("Get resources")
                .WaitForResources("Worker")
                .Sequence("Collect and wait for resources")
                    .Splice(PrimitiveActions.CollectFoodAction)
                    .WaitForResources("Worker")
                .End()
                .Sequence("Re-assign worker to get resources")
                    .UnassignWorker()
                    .Splice(PrimitiveActions.CollectFoodAction)
                    .WaitForResources("Worker")
                .End()
            .End()
            .Build();

        return new AIDomainBuilder("Recruit Worker Domain")
            .Select("Recruit worker")
                .Splice(PrimitiveActions.RecruitWorkerAction)

                .Sequence("Get resources and recruit worker")
                    .CanRecruit("Worker")
                    .Splice(getResourcesForWorker)
                    .Splice(PrimitiveActions.RecruitWorkerAction)
                .End()
            .End()
            .Build();
    }
}

