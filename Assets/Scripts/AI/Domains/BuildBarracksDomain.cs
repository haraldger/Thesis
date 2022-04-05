using System;
using FluidHTN;

public static class BuildBarracksDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Build Barracks Domain")
            .Select("Build barracks")
                .Splice(PrimitiveActions.BuildBarracksAction)
                .Sequence("Wait for resources and build barracks")
                    .HasFreeBuildingSpot()
                    .WaitForResources("Barracks")
                    .Splice(PrimitiveActions.BuildBarracksAction)
                .End()
                .Sequence("Collect resources and build barracks")
                    .HasFreeBuildingSpot()
                    .Splice(CollectWoodDomain.Create())
                    .Splice(CollectGoldDomain.Create())
                    .WaitForResources("Barracks")
                    .Splice(PrimitiveActions.BuildBarracksAction)
                .End()
            .End()
            .Build();
    }
}

