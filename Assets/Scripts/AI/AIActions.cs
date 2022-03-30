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

    public static TaskStatus BuildCitadel(AIContext context)
    {
        return AIManager.Instance.BuildBuilding("Citadel");
    }

    public static TaskStatus RecruitSwordsman(AIContext context)
    {
        return AIManager.Instance.RecruitTroop("Swordsman");
    }

    public static TaskStatus RecruitRanger(AIContext context)
    {
        return AIManager.Instance.RecruitTroop("Ranger");
    }

    public static TaskStatus RecruitWorker(AIContext context)
    {
        return AIManager.Instance.RecruitTroop("Worker");
    }

    public static TaskStatus CollectGold(AIContext context)
    {
        return AIManager.Instance.CollectResource("Gold");
    }

    public static TaskStatus CollectWood(AIContext context)
    {
        return AIManager.Instance.CollectResource("Wood");
    }

    public static TaskStatus CollectFood(AIContext context)
    {
        return AIManager.Instance.CollectResource("Food");
    }
}
