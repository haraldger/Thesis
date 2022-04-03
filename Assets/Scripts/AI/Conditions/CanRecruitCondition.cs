using System;
using FluidHTN;
using FluidHTN.Conditions;

public class CanRecruitCondition : ICondition
{
    public string Name { get; }

    public string TroopType { get; }

    public CanRecruitCondition(string troopType)
    {
        Name = $"Can recruit {troopType}";
        TroopType = troopType;
    }

    public bool IsValid(IContext ctx)
    {
        if (ctx is AIContext context)
        {
            bool result = false;

            switch (TroopType)
            {
                case "Worker":
                    result = context.HasState(AIWorldState.CanRecruitWorker);
                    break;

                case "Swordsman":
                    result = context.HasState(AIWorldState.CanRecruitSwordsman);
                    break;

                case "Ranger":
                    result = context.HasState(AIWorldState.CanRecruitRanger);
                    break;

                default:
                    break;
            }

            if (context.LogDecomposition) context.Log(Name, $"CanRecruitCondition.IsValid({TroopType}:{result})", context.CurrentDecompositionDepth + 1, this);
            return result;
        }

        throw new Exception($"Unexpected context type {ctx}");
    }
}

