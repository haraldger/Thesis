using System;
using FluidHTN;

public static class BuildCitadelDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Build Citadel Domain")
            .Select("Build Citadel")
                .Splice(PrimitiveActions.BuildCitadelAction)
                .Sequence("Wait for resources and build citadel")
                    .HasFreeBuildingSpot()
                    .WaitForResources("Citadel")
                    .Splice(PrimitiveActions.BuildCitadelAction)
                .End()
                .Sequence("Collect resources and build citadel")
                    .HasFreeBuildingSpot()
                    .Splice(CollectWoodDomain.Create())
                    .Splice(CollectGoldDomain.Create())
                    .WaitForResources("Citadel")
                    .Splice(PrimitiveActions.BuildCitadelAction)
                .End()
            .End()
            .Build();
    }
}

