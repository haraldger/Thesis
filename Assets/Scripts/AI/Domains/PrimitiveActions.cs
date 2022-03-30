using System;
using FluidHTN;

public static class PrimitiveActions
{
    public static Domain<AIContext> BuildBarracksAction = new DomainBuilder<AIContext>("Build Barracks Primitive Action")
        .Action("Build Barracks")
            .Condition("Can build barracks", ctx => ctx.HasState(AIWorldState.CanBuildBarracks))
            .Do(AIActions.BuildBarracks)
        .End()
        .Build();

    public static Domain<AIContext> BuildFarmAction = new DomainBuilder<AIContext>("Build Farm Primitive Action")
        .Action("Build Farm")
            .Condition("Can build farm", ctx => ctx.HasState(AIWorldState.CanBuildFarm))
            .Do(AIActions.BuildFarm)
        .End()
        .Build();

    public static Domain<AIContext> BuildCitadelAction = new DomainBuilder<AIContext>("Build Citadel Primitive Action")
        .Action("Build Citadel")
            .Condition("Can build citadel", ctx => ctx.HasState(AIWorldState.CanBuildCitadel))
            .Do(AIActions.BuildCitadel)
        .End()
        .Build();

    public static Domain<AIContext> RecruitSwordsmanAction = new DomainBuilder<AIContext>("Recruit Swordsman Primitive Action")
        .Action("Recruit Swordsman")
            .Condition("Can recruit swordsman", ctx => ctx.HasState(AIWorldState.CanRecruitSwordsman))
            .Do(AIActions.RecruitSwordsman)
        .End()
        .Build();

    public static Domain<AIContext> RecruitRangerAction = new DomainBuilder<AIContext>("Recruit Ranger Primitive Action")
        .Action("Recruit Ranger")
            .Condition("Can recruit ranger", ctx => ctx.HasState(AIWorldState.CanRecruitRanger))
            .Do(AIActions.RecruitRanger)
        .End()
        .Build();

    public static Domain<AIContext> RecruitWorkerAction = new DomainBuilder<AIContext>("Recruit Worker Primitive Action")
        .Action("Recruit Worker")
            .Condition("Can recruit worker", ctx => ctx.HasState(AIWorldState.CanRecruitWorker))
            .Do(AIActions.RecruitWorker)
        .End()
        .Build();

    public static Domain<AIContext> CollectGoldAction = new DomainBuilder<AIContext>("Collect Gold Primitive Action")
        .Action("Collect Gold")
            .Condition("Can collect gold", ctx => ctx.HasState(AIWorldState.CanCollectGold))
            .Do(AIActions.CollectGold)
        .End()
        .Build();

    public static Domain<AIContext> CollectWoodAction = new DomainBuilder<AIContext>("Collect Wood Primitive Action")
        .Action("Collect Wood")
            .Condition("Can collect wood", ctx => ctx.HasState(AIWorldState.CanCollectWood))
            .Do(AIActions.CollectWood)
        .End()
        .Build();

    public static Domain<AIContext> CollectFoodAction = new DomainBuilder<AIContext>("Collect Food Primitive Action")
        .Action("Collect Food")
            .Condition("Can collect food", ctx => ctx.HasState(AIWorldState.CanCollectFood))
            .Do(AIActions.CollectFood)
        .End()
        .Build();

}

