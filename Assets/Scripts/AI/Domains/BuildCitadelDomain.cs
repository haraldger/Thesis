using System;
using FluidHTN;

public static class BuildCitadelDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Build Citadel Domain")
            .Select("Build Citadel")
                .Splice(PrimitiveActions.BuildCitadelAction)

                .Sequence("Get resources and build citadel")
                    .HasFreeBuildingSpot()

                    .Select("Get resources")
                        .WaitForResources("Citadel")
                        .Sequence("Collect resources")
                            .Splice(CollectGoldDomain.Create())
                            .Splice(CollectWoodDomain.Create())
                            .WaitForResources("Citadel")
                        .End()
                    .End()

                    .Splice(PrimitiveActions.BuildCitadelAction)
                .End()
            .End()
            .Build();
    }
}

