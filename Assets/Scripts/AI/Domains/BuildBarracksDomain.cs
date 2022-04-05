using System;
using FluidHTN;

public class BuildBarracksDomain : AbstractDomain
{
    public override Domain<AIContext, int> Domain { get; set; }

    void Awake()
    {
        Domain = DefineDomain();
    }

    private Domain<AIContext, int> DefineDomain()
    {
        return new AIDomainBuilder("Build Barracks Domain")
            .Select("Build barracks")
                .Splice(PrimitiveActions.BuildBarracksAction)
                .Action("Collect resources and build barracks")
                    .HasFreeBuildingSpot()

                .End()
            .Build();
    }
}

