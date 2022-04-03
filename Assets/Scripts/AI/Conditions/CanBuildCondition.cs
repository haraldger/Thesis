using System;
using FluidHTN;
using FluidHTN.Conditions;

public class CanBuildCondition : ICondition
{

    public string Name { get; }

    public string BuildingType { get; }

    public CanBuildCondition(string buildingType)
    {
        BuildingType = buildingType;
        Name = $"Can build {buildingType}";
    }

    public bool IsValid(IContext ctx)
    {
        if (ctx is AIContext context)
        {
            bool result = false;

            switch (BuildingType)
            {
                case "Barracks":
                    result = context.HasState(AIWorldState.CanBuildBarracks);
                    break;

                case "Farm":
                    result = context.HasState(AIWorldState.CanBuildFarm);
                    break;

                case "Citadel":
                    result = context.HasState(AIWorldState.CanBuildCitadel);
                    break;

                default:
                    break;
            }

            if (context.LogDecomposition) context.Log(Name, $"CanBuildCondition.IsValid({BuildingType}:{result})", context.CurrentDecompositionDepth+1, this);
            return result;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

