using System;
using FluidHTN;

public static class PrimitiveActions
{
    public static Domain<AIContext, int> BuildBarracksAction = new AIDomainBuilder("Build Barracks Primitive Action")
        .Action("Build Barracks")
            .CanBuild("Barracks")
            .Do(AIActions.BuildBarracks)
        .End()
        .Build();

    public static Domain<AIContext, int> BuildFarmAction = new AIDomainBuilder("Build Farm Primitive Action")
        .Action("Build Farm")
            .CanBuild("Farm")
            .Do(AIActions.BuildFarm)
        .End()
        .Build();

    public static Domain<AIContext, int> BuildCitadelAction = new AIDomainBuilder("Build Citadel Primitive Action")
        .Action("Build Citadel")
            .CanBuild("Citadel")
            .Do(AIActions.BuildCitadel)
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitSwordsmanAction = new AIDomainBuilder("Recruit Swordsman Primitive Action")
        .Action("Recruit Swordsman")
            .Condition("Can recruit swordsman", ctx => ctx.HasState(AIWorldState.CanRecruitSwordsman))
            .Do(AIActions.RecruitSwordsman)
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitRangerAction = new AIDomainBuilder("Recruit Ranger Primitive Action")
        .Action("Recruit Ranger")
            .Condition("Can recruit ranger", ctx => ctx.HasState(AIWorldState.CanRecruitRanger))
            .Do(AIActions.RecruitRanger)
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitWorkerAction = new AIDomainBuilder("Recruit Worker Primitive Action")
        .Action("Recruit Worker")
            .Condition("Can recruit worker", ctx => ctx.HasState(AIWorldState.CanRecruitWorker))
            .Do(AIActions.RecruitWorker)
        .End()
        .Build();

    public static Domain<AIContext, int> CollectGoldAction = new AIDomainBuilder("Collect Gold Primitive Action")
        .Action("Collect Gold")
            .Condition("Can collect gold", ctx => ctx.HasState(AIWorldState.CanCollectGold))
            .Do(AIActions.CollectGold)
        .End()
        .Build();

    public static Domain<AIContext, int> CollectWoodAction = new AIDomainBuilder("Collect Wood Primitive Action")
        .Action("Collect Wood")
            .Condition("Can collect wood", ctx => ctx.HasState(AIWorldState.CanCollectWood))
            .Do(AIActions.CollectWood)
        .End()
        .Build();

    public static Domain<AIContext, int> CollectFoodAction = new AIDomainBuilder("Collect Food Primitive Action")
        .Action("Collect Food")
            .Condition("Can collect food", ctx => ctx.HasState(AIWorldState.CanCollectFood))
            .Do(AIActions.CollectFood)
        .End()
        .Build();

}

