using System;
using FluidHTN;
using FluidHTN.Factory;
using FluidHTN.PrimitiveTasks;

public class AIDomainBuilder : BaseDomainBuilder<AIDomainBuilder,AIContext>
{
    public AIDomainBuilder(string domainName) : base(domainName, new DefaultFactory())
    {
    }


    //============================================================= CONDITIONS

    public AIDomainBuilder CanBuild(string buildingType)
    {
        var condition = new CanBuildCondition(buildingType);
        Pointer.AddCondition(condition);
        return this;
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

    public AIDomainBuilder UpdateWorldState()
    {
        if (Pointer is IPrimitiveTask task)
        {
            var effect = new UpdateWorldStateEffect();
            task.AddEffect(effect);
        }
        return this;
    }
}

