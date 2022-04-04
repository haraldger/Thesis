using System;
using FluidHTN;

public class SimpleBuildDomain : AbstractDomain
{
    public override Domain<AIContext, int> Domain { get; set; } 

    void Awake()
    {
        Domain = DefineDomain();
    }

    private Domain<AIContext, int> DefineDomain()
    {
        var buildBarracksActionDomain = new AIDomainBuilder("Build Barracks Domain")
            .Action("Build Barracks")
                .CanBuild("Barracks")
                .Do(AIActions.BuildBarracks)
                .BuyBuilding("Barracks")
            .End()
            .Build();

        var buildFarmActionDomain = new AIDomainBuilder("Build Farm Domain")
            .Action("Build Farm")
                .CanBuild("Farm")
                .Do(AIActions.BuildFarm)
                .BuyBuilding("Farm")
            .End()
            .Build();

        return new AIDomainBuilder("Simple Build Domain")
            .Sequence("Build 2 barracks and 2 farms")
                .Splice(buildBarracksActionDomain)
                .Splice(buildBarracksActionDomain)
                .Splice(buildFarmActionDomain)
                .Splice(buildFarmActionDomain)
            .End()
            .Build();

    }
}

