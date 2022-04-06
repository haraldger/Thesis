using System;
using FluidHTN;

public class ComplexDomain : AbstractDomain
{
    public override Domain<AIContext, int> Domain { get; set; }

    private void Awake()
    {
        Domain = DefineDomain();
    }

    private Domain<AIContext, int> DefineDomain()
    {
        return new AIDomainBuilder("Complex Domain")
            .Sequence("Domain Sequence")
                .Splice(CollectFoodDomain.Create())
            .End()
            .Build();
    }
}

