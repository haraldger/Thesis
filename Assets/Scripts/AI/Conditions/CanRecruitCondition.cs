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
            bool hasBuilding = context.HasBuildingType(buildingType);
            if (!hasBuilding)
            {
                if (context.LogDecomposition) context.Log(Name, $"No available building {buildingType} to recruit {TroopType}", context.CurrentDecompositionDepth + 1, this);
                return false;
            }


            if (context.LogDecomposition) context.Log(Name, $"CanRecruitCondition.IsValid:True", context.CurrentDecompositionDepth + 1, this);
            return true;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

