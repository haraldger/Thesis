using System;
using FluidHTN;

public class SimpleResourceDomain : AbstractDomain
{
    public override Domain<AIContext, int> Domain { get; set; }

    void Awake()
    {
        Domain = DefineDomain();
    }

    private Domain<AIContext, int> DefineDomain()
    {
        return new AIDomainBuilder("Simple Resource Domain")
            .Sequence("Recruit worker and harvest wood")
                .Splice(PrimitiveActions.RecruitWorkerAction)
                .Splice(PrimitiveActions.CollectWoodAction)
            .End()
            .Build();
    }
}

