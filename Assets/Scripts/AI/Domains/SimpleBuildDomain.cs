using System;
using FluidHTN;

public static class SimpleBuildDomain
{
    public static Domain<AIContext> Domain { get; private set; } = DefineDomain();

    private static Domain<AIContext> DefineDomain()
    {
        return new DomainBuilder<AIContext>("Simple Build Domain")
            .Select("Build Barracks")
                .Action("Build Barracks")
                    .Condition("Can build barracks", ctx => ctx.HasState(AIWorldState.CanBuildBarracks))
                    .Do(AIActions.BuildBarracks)
                    .Effect("Add barracks", EffectType.PlanAndExecute, (ctx,type) => { ctx.AddBuilding("Barracks"); })
                .End()
                .Action("Build Farm")
                    .Condition("Can build farm", ctx => ctx.HasState(AIWorldState.CanBuildFarm))
                    .Do(AIActions.BuildFarm)
                    .Effect("Add farm", EffectType.PlanAndExecute, (ctx, type) => { ctx.AddBuilding("Farm"); })
                .End()
            .End()
            .Build();

    }
}

