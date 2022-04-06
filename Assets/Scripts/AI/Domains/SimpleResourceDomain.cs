using System;
using FluidHTN;

public class SimpleResourceDomain : AbstractDomain
{
    public override Domain<AIContext> Domain { get; set; }

    void Awake()
    {
        Domain = DefineDomain();
    }

    private Domain<AIContext> DefineDomain()
    {
        return new DomainBuilder<AIContext>("Simple Resource Domain")
            .Sequence("Recruit worker and harvest wood")
                .Splice(PrimitiveActions.RecruitWorkerAction)
            .End()
            .Build();
    }
}

