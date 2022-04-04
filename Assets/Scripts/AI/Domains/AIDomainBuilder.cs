using System;
using FluidHTN;
using FluidHTN.Factory;
using FluidHTN.PrimitiveTasks;

public class AIDomainBuilder : BaseDomainBuilder<AIDomainBuilder, AIContext, int>
{
    public AIDomainBuilder(string domainName) : base(domainName, new DefaultFactory())
    {
    }

    public AIDomainBuilder CanRecruit(string troopType)
    {
        var condition = new CanRecruitCondition(troopType);
        Pointer.AddCondition(condition);
        return this;
    }

    public AIDomainBuilder CanCollect(string resourceType)
    {
        var condition = new CanCollectCondition(resourceType);
        Pointer.AddCondition(condition);
        return this;
    }

    public AIDomainBuilder CanAffordBuilding(string buildingType)
    {
        var condition = new CanAffordBuildingCondition(buildingType);
        Pointer.AddCondition(condition);
        return this;
    }

    public AIDomainBuilder CanAffordTroop(string troopType)
    {
        var condition = new CanAffordTroopCondition(troopType);
        Pointer.AddCondition(condition);
        return this;
    }

    public AIDomainBuilder HasFreeBuildingSpot()
    {
        var condition = new HasFreeBuildingSpotCondition();
        Pointer.AddCondition(condition);
        return this;
    }

    public AIDomainBuilder HasIdleWorker()
    {
        var condition = new HasIdleWorkerCondition();
        Pointer.AddCondition(condition);
        return this;
    }

    public AIDomainBuilder BuyTroop(string troopType, EffectType type = EffectType.PlanAndExecute)
    {
        if (Pointer is IPrimitiveTask<int> task)
        {
            var effect = new BuyTroopEffect(troopType, type);
            task.AddEffect(effect);
        }
        return this;
    }

    public AIDomainBuilder BuyBuilding(string buildingType, EffectType type = EffectType.PlanAndExecute)
    {
        if (Pointer is IPrimitiveTask<int> task)
        {
            var effect = new BuyBuildingEffect(buildingType, type);
            task.AddEffect(effect);
        }
        return this;
    }

    public AIDomainBuilder MakeWorkerBusy(EffectType type = EffectType.PlanAndExecute)
    {
        if (Pointer is IPrimitiveTask<int> task)
        {
            var effect = new MakeWorkerBusyEffect(type);
            task.AddEffect(effect);
        }
        return this;
    }

    public AIDomainBuilder MakeWorkerIdle(EffectType type = EffectType.PlanAndExecute)
    {
        if (Pointer is IPrimitiveTask<int> task)
        {
            var effect = new MakeWorkerIdleEffect(type);
            task.AddEffect(effect);
        }
        return this;
    }

    public AIDomainBuilder MakeBuildingSpotFree(EffectType type = EffectType.PlanAndExecute)
    {
        if (Pointer is IPrimitiveTask<int> task)
        {
            var effect = new MakeBuildingSpotFreeEffect(type);
            task.AddEffect(effect);
        }
        return this;
    }

    public AIDomainBuilder MakeBuildingSpotOccupied(EffectType type = EffectType.PlanAndExecute)
    {
        if (Pointer is IPrimitiveTask<int> task)
        {
            var effect = new MakeBuildingSpotOccupiedEffect(type);
            task.AddEffect(effect);
        }
        return this;
    }

}

