using System;
using FluidHTN;
using FluidHTN.Conditions;

public static class PrimitiveActions
{
    public static Domain<AIContext> BuildBarracksAction = new AIDomainBuilder("Build Barracks Primitive Action")
        .Action("Build Barracks")
            .CanBuild("Barracks")
            .Do(AIActions.BuildBarracks)
            .UpdateWorldState()
        .End()
        .Build();

    public static Domain<AIContext> BuildFarmAction = new AIDomainBuilder("Build Farm Primitive Action")
        .Action("Build Farm")
            .CanBuild("Farm")
            .Do(AIActions.BuildFarm)
            .UpdateWorldState()
        .End()
        .Build();

    public static Domain<AIContext> BuildCitadelAction = new AIDomainBuilder("Build Citadel Primitive Action")
        .Action("Build Citadel")
            .CanBuild("Citadel")
            .Do(AIActions.BuildCitadel)
            .UpdateWorldState()
        .End()
        .Build();

    public static Domain<AIContext> RecruitSwordsmanAction = new AIDomainBuilder("Recruit Swordsman Primitive Action")
        .Action("Recruit Swordsman")
            .Condition("Can recruit swordsman", ctx => ctx.HasState(AIWorldState.CanRecruitSwordsman))
            .Do(AIActions.RecruitSwordsman)
            .UpdateWorldState()
        .End()
        .Build();

    public static Domain<AIContext> RecruitRangerAction = new AIDomainBuilder("Recruit Ranger Primitive Action")
        .Action("Recruit Ranger")
            .CanRecruit("Ranger")
            .Do(AIActions.RecruitRanger)
        .End()
        .Build();

    public static Domain<AIContext> RecruitWorkerAction = new AIDomainBuilder("Recruit Worker Primitive Action")
        .Action("Recruit Worker")
            .CanRecruit("Worker")
            .Do(AIActions.RecruitWorker)
        .End()
        .Build();

    public static Domain<AIContext> CollectGoldAction = new AIDomainBuilder("Collect Gold Primitive Action")
        .Action("Collect Gold")
            .CanCollect("Gold")
            .Do(AIActions.CollectGold)
        .End()
        .Build();

    public static Domain<AIContext> CollectWoodAction = new AIDomainBuilder("Collect Wood Primitive Action")
        .Action("Collect Wood")
            .CanCollect("Wood")
            .Do(AIActions.CollectWood)
        .End()
        .Build();

    public static Domain<AIContext> CollectFoodAction = new AIDomainBuilder("Collect Food Primitive Action")
        .Action("Collect Food")
            .CanCollect("Food")
            .Do(AIActions.CollectFood)
        .End()
        .Build();

}
