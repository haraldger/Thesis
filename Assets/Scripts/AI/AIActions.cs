using System;
using FluidHTN;
using UnityEngine;

public static class AIActions
{
    public static TaskStatus BuildBarracks(AIContext context)
    {
        return AIManager.Instance.BuildBuilding("Barracks");
    }

    public static TaskStatus BuildFarm(AIContext context)
    {
        return AIManager.Instance.BuildBuilding("Farm");
    }
}
