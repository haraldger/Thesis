using System;
using FluidHTN;

public static class PrimitiveActions
{
    public static Domain<AIContext, int> BuildBarracksAction = new AIDomainBuilder("Build Barracks Primitive Action")
        .Action("Build Barracks")
            .CanBuild("Barracks")
            .Do(AIActions.BuildBarracks)
            .BuyBuilding("Barracks")
            .MakeBuildingSpotOccupied()
        .End()
        .Build();

    public static Domain<AIContext, int> BuildFarmAction = new AIDomainBuilder("Build Farm Primitive Action")
        .Action("Build Farm")
            .CanBuild("Farm")
            .Do(AIActions.BuildFarm)
            .BuyBuilding("Farm")
            .MakeBuildingSpotOccupied()
        .End()
        .Build();

    public static Domain<AIContext, int> BuildCitadelAction = new AIDomainBuilder("Build Citadel Primitive Action")
        .Action("Build Citadel")
            .CanBuild("Citadel")
            .Do(AIActions.BuildCitadel)
            .BuyBuilding("Citadel")
            .MakeBuildingSpotOccupied()
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitSwordsmanAction = new AIDomainBuilder("Recruit Swordsman Primitive Action")
        .Action("Recruit Swordsman")
            .CanRecruit("Swordsman")
            .Do(AIActions.RecruitSwordsman)
            .BuyTroop("Swordsman")
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitRangerAction = new AIDomainBuilder("Recruit Ranger Primitive Action")
        .Action("Recruit Ranger")
            .CanRecruit("Ranger")
            .Do(AIActions.RecruitRanger)
            .BuyTroop("Ranger")
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitWorkerAction = new AIDomainBuilder("Recruit Worker Primitive Action")
        .Action("Recruit Worker")
            .CanRecruit("Worker")
            .Do(AIActions.RecruitWorker)
            .BuyTroop("Worker")
        .End()
        .Build();

    public static Domain<AIContext, int> CollectGoldAction = new AIDomainBuilder("Collect Gold Primitive Action")
        .Action("Collect Gold")
            .CanCollect("Gold")
            .Do(AIActions.CollectGold)
            .MakeWorkerBusy()
        .End()
        .Build();

    public static Domain<AIContext, int> CollectWoodAction = new AIDomainBuilder("Collect Wood Primitive Action")
        .Action("Collect Wood")
            .CanCollect("Wood")
            .Do(AIActions.CollectWood)
            .MakeWorkerBusy()
        .End()
        .Build();

    public static Domain<AIContext, int> CollectFoodAction = new AIDomainBuilder("Collect Food Primitive Action")
        .Action("Collect Food")
            .CanCollect("Food")
            .Do(AIActions.CollectFood)
            .MakeWorkerBusy()
        .End()
        .Build();

}

