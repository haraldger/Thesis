using System;
using UnityEngine;

public static class DataHandler
{
    public static void LoadGameData()
    {
        Globals.BUILDING_DATA = Resources.LoadAll<BuildingData>("ScriptableObjects/Buildings") as BuildingData[];
        Globals.TROOP_DATA = Resources.LoadAll<TroopData>("ScriptableObjects/Troops") as TroopData[];
        Globals.UNIT_DATA = Resources.LoadAll<GameUnitData>("ScriptableObjects") as GameUnitData[];
    }
}

