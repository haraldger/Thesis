using System;
using FluidHTN;
using FluidHTN.Effects;

public class BuyBuildingEffect : IEffect<int>
{
    public string Name { get; }

    public string BuildingType { get; }

    public EffectType Type { get; }

    public BuyBuildingEffect(string buildingType, EffectType type = EffectType.PlanAndExecute)
    {
        Name = $"Buy building {buildingType}";
        BuildingType = buildingType;
        Type = type;
    }

    public void Apply(IContext<int> ctx)
    {
        if (ctx is AIContext context)
        {
            GameUnitData building = Array.Find(Globals.BUILDING_DATA, data => data.code == BuildingType);
            var costs = building.costs;
            foreach (var cost in costs)
            {
                context.RemoveResource(cost.code, cost.value, Type);
            }

            if (context.LogDecomposition) context.Log(Name, $"BuyBuildingEffect.Apply({BuildingType})", context.CurrentDecompositionDepth + 1, this);
            return;
         }

        throw new Exception($"Unexpected context type {ctx}");
    }

}

