using System;
using System.Collections.Generic;
using System.Linq;
using FluidHTN;
using FluidHTN.Conditions;

public class CanRecruitCondition : ICondition<int>
{
    public string Name { get; }

    public string TroopType { get; }

    public CanRecruitCondition(string troopType)
    {
        Name = $"Can recruit {troopType}";
        TroopType = troopType;
    }

    public bool IsValid(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            // Check building constraints
            var recruitingBuilding = Array.Find(Globals.BUILDING_DATA, data => data.recruitingOptions.Select(option => option.code).Contains(TroopType));
            var buildingType = recruitingBuilding.code;
            bool result = context.HasBuildingType(buildingType);


            if (context.LogDecomposition) context.Log(Name, $"CanRecruitCondition.IsValid({TroopType}):{result}", context.CurrentDecompositionDepth + 1, this);
            return result;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

