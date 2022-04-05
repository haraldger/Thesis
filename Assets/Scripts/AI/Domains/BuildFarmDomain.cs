using System;
using FluidHTN;

public class BuildFarmDomain
{
    public static Domain<AIContext, int> Create()
    {
        return new AIDomainBuilder("Build Farm Domain")
            .Select("Build Farm")
                .Splice(PrimitiveActions.BuildFarmAction)
                .Sequence("Wait for resources and build farm")
                    .HasFreeBuildingSpot()
                    .WaitForResources("Farm")
                    .Splice(PrimitiveActions.BuildFarmAction)
                .End()
                .Sequence("Collect resources and build farm")
                    .HasFreeBuildingSpot()
                    .Splice(CollectWoodDomain.Create())
                    .WaitForResources("Farm")
                    .Splice(PrimitiveActions.BuildFarmAction)
                .End()
            .End()
            .Build();
    }
}

