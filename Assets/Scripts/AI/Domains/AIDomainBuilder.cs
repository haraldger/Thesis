using System;
using FluidHTN;
using FluidHTN.Factory;

public class AIDomainBuilder : BaseDomainBuilder<AIDomainBuilder, AIContext, int>
{
    public AIDomainBuilder(string domainName) : base(domainName, new DefaultFactory())
    {
    }

    public AIDomainBuilder CanBuild(string buildingType)
    {
        var condition = new CanBuildCondition(buildingType);
        Pointer.AddCondition(condition);
        return this;
    }
}

