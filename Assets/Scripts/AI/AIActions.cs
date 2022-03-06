using System;
using FluidHTN;
using UnityEngine;

public static class AIActions
{
    public static TaskStatus BuildBarracks(AIContext context)
    {
        AIManager.Instance.BuildBuilding("Barracks");
        return TaskStatus.Success;
    }

    public static TaskStatus BuildFarm(AIContext context)
    {
        AIManager.Instance.BuildBuilding("Farm");
        return TaskStatus.Success; 
    }
}
