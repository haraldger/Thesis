using System;
using FluidHTN;

public static class BuildBarracksDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Build Barracks Domain")
            .Select("Build barracks")
                .Splice(PrimitiveActions.BuildBarracksAction)

                .Sequence("Get resources and build barracks")
                    .HasFreeBuildingSpot()

                    .Select("Get resources")
                        .WaitForResources("Barracks")
                        .Sequence("Collect resources")
                            .Splice(CollectGoldDomain.Create())
                            .Splice(CollectWoodDomain.Create())
                            .WaitForResources("Barracks")
                        .End()
                    .End()

                    .Splice(PrimitiveActions.BuildBarracksAction)
                .End()
            .End()
            .Build();
    }
}

