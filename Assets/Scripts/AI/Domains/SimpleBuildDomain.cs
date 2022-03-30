using System;
using FluidHTN;

public class SimpleBuildDomain : AbstractDomain
{
    public override Domain<AIContext> Domain { get; set; } 

    void Awake()
    {
        Domain = DefineDomain();
    }

    private Domain<AIContext> DefineDomain()
    {
        var buildBarracksActionDomain = new DomainBuilder<AIContext>("Build Barracks Domain")
            .Action("Build Barracks")
                .Condition("Can build barracks", ctx => ctx.HasState(AIWorldState.CanBuildBarracks))
                .Do(AIActions.BuildBarracks)
            .End()
            .Build();

        var buildFarmActionDomain = new DomainBuilder<AIContext>("Build Farm Domain")
            .Action("Build Farm")
                .Condition("Can build farm", ctx => ctx.HasState(AIWorldState.CanBuildFarm))
                .Do(AIActions.BuildFarm)
            .End()
            .Build();

        return new DomainBuilder<AIContext>("Simple Build Domain")
            .Sequence("Build 2 barracks and 2 farms")
                .Splice(buildBarracksActionDomain)
                .Splice(buildBarracksActionDomain)
                .Splice(buildFarmActionDomain)
                .Splice(buildFarmActionDomain)
            .End()
            .Build();

    }
}

