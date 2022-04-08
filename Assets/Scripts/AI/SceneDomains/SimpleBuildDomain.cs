﻿using System;
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
                .CanAffordBuilding("Barracks")
                .HasFreeBuildingSpot()
                .Do(AIActions.BuildBarracks)
                .BuyBuilding("Barracks")
                .MakeBuildingSpotOccupied()
            .End()
            .Build();

        var buildFarmActionDomain = new AIDomainBuilder("Build Farm Domain")
            .Action("Build Farm")
                .CanAffordBuilding("Farm")
                .HasFreeBuildingSpot()
                .Do(AIActions.BuildFarm)
                .BuyBuilding("Farm")
                .MakeBuildingSpotOccupied()
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
