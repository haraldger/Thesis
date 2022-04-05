using System;
using FluidHTN;

public class CollectGoldDomain : AbstractDomain
{
    public override Domain<AIContext, int> Domain { get; set; }

    void Awake()
    {
        Domain = DefineDomain();
    }

    private Domain<AIContext, int> DefineDomain()
    {
        return new AIDomainBuilder("Collect Gold Domain")
            .Select("Collect Gold")
                .Splice(PrimitiveActions.CollectGoldAction)
                .Sequence("Re-assign worker to collect gold")
                    .CanCollect("Gold")
                    
                .End()
            .Build();
    }

}

