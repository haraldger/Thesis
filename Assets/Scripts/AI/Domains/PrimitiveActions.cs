using System;
using FluidHTN;

public static class PrimitiveActions
{
    public static Domain<AIContext, int> BuildBarracksAction = new AIDomainBuilder("Build Barracks Primitive Action")
        .Action("Build Barracks")
            .CanAffordBuilding("Barracks")
            .HasFreeBuildingSpot()
            .Do(AIActions.BuildBarracks)
            .BuyBuilding("Barracks")
            .MakeBuildingSpotOccupied()
        .End()
        .Build();

    public static Domain<AIContext, int> BuildFarmAction = new AIDomainBuilder("Build Farm Primitive Action")
        .Action("Build Farm")
            .CanAffordBuilding("Farm")
            .HasFreeBuildingSpot()
            .Do(AIActions.BuildFarm)
            .BuyBuilding("Farm")
            .MakeBuildingSpotOccupied()
        .End()
        .Build();

    public static Domain<AIContext, int> BuildCitadelAction = new AIDomainBuilder("Build Citadel Primitive Action")
        .Action("Build Citadel")
            .CanAffordBuilding("Citadel")
            .HasFreeBuildingSpot()
            .Do(AIActions.BuildCitadel)
            .BuyBuilding("Citadel")
            .MakeBuildingSpotOccupied()
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitSwordsmanAction = new AIDomainBuilder("Recruit Swordsman Primitive Action")
        .Action("Recruit Swordsman")
            .CanRecruit("Swordsman")
            .CanAffordTroop("Swordsman")
            .Do(AIActions.RecruitSwordsman)
            .BuyTroop("Swordsman")
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitRangerAction = new AIDomainBuilder("Recruit Ranger Primitive Action")
        .Action("Recruit Ranger")
            .CanRecruit("Ranger")
            .CanAffordTroop("Ranger")
            .Do(AIActions.RecruitRanger)
            .BuyTroop("Ranger")
        .End()
        .Build();

    public static Domain<AIContext, int> RecruitWorkerAction = new AIDomainBuilder("Recruit Worker Primitive Action")
        .Action("Recruit Worker")
            .CanRecruit("Worker")
            .CanAffordTroop("Worker")
            .Do(AIActions.RecruitWorker)
            .BuyTroop("Worker")
            .MakeWorkerIdle()
        .End()
        .Build();

    public static Domain<AIContext, int> CollectGoldAction = new AIDomainBuilder("Collect Gold Primitive Action")
        .Action("Collect Gold")
            .CanCollect("Gold")
            .HasIdleWorker()
            .Do(AIActions.CollectGold)
            .MakeWorkerBusy()
        .End()
        .Build();

    public static Domain<AIContext, int> CollectWoodAction = new AIDomainBuilder("Collect Wood Primitive Action")
        .Action("Collect Wood")
            .CanCollect("Wood")
            .HasIdleWorker()
            .Do(AIActions.CollectWood)
            .MakeWorkerBusy()
        .End()
        .Build();

    public static Domain<AIContext, int> CollectFoodAction = new AIDomainBuilder("Collect Food Primitive Action")
        .Action("Collect Food")
            .CanCollect("Food")
            .HasIdleWorker()
            .Do(AIActions.CollectFood)
            .MakeWorkerBusy()
        .End()
        .Build();

}

