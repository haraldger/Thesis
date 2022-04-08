using System;
using FluidHTN;

public static class RecruitSwordsmanDomain
{
    public static Domain<AIContext, int> Create()
    {
        var getResourcesForSwordsman = new AIDomainBuilder("Get resources for swordsman")
            .Select("Get resources")
                .WaitForResources("Swordsman")
                .Sequence("Collect resources")
                    .Splice(CollectFoodDomain.Create())
                    .Splice(CollectGoldDomain.Create())
                    .WaitForResources("Swordsman")
                .End()
            .End()
            .Build();

        return new AIDomainBuilder("Recruit Swordsman Domain")
            .Select("Recruit swordsman")
                .Splice(PrimitiveActions.RecruitSwordsmanAction)

                .Sequence("Get resources and recruit swordsman")
                    .CanRecruit("Swordsman")
                    .Splice(getResourcesForSwordsman)
                    .Splice(PrimitiveActions.RecruitSwordsmanAction)
                .End()

                .Sequence("Build barracks and recruit swordsman")
                    .Splice(BuildBarracksDomain.Create())
                    .Splice(getResourcesForSwordsman)
                    .Splice(PrimitiveActions.RecruitSwordsmanAction)
                .End()
            .End()
            .Build();
    }

}

