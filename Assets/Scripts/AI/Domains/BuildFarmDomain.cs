using System;
using FluidHTN;

public class BuildFarmDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Build Farm Domain")
            .Select("Build Farm")
                .Splice(PrimitiveActions.BuildFarmAction)

                .Sequence("Get resources and build farm")
                    .HasFreeBuildingSpot()

                    .Select("Get resources")
                        .WaitForResources("Farm")
                        .Sequence("Collect resources")
                            .Splice(CollectWoodDomain.Create())
                            .WaitForResources("Farm")
                        .End()
                    .End()

                    .Splice(PrimitiveActions.BuildFarmAction)
                .End()
            .End()
            .Build();
    }
}

