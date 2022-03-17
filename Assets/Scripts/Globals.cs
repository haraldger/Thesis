using System.Collections.Generic;

public class Globals 
{
    public static IDictionary<string, GameResourceData> RESOURCE_DATA = new Dictionary<string, GameResourceData>()
    {
        {"Wood", new GameResourceData("Wood", 2000, 200)},
        {"Gold", new GameResourceData("Gold", 2000, 50)},
        {"Food", new GameResourceData("Food", 2000, 250)}
    };

    public static BuildingData[] BUILDING_DATA;

    public static TroopData[] TROOP_DATA;

    public static IList<Building> CURRENT_BUILDINGS = new List<Building>();

    //public static IDictionary<string, GameUnitData> BUILDING_DATA = new Dictionary<string, GameUnitData>()
    //{
    //    {"Barracks", new BuildingData(
    //        "Barracks",
    //        100,
    //        new CostValue("Wood", 200),
    //        new CostValue("Gold", 50))},

    //    {"Citadel", new BuildingData(
    //        "Citadel",
    //        1000,
    //        new CostValue("Wood", 500),
    //        new CostValue("Gold", 250))},

    //    {"Farm", new BuildingData(
    //        "Farm",
    //        75,
    //        new CostValue("Wood", 50))}
    //};

    //public static IDictionary<string, GameUnitData> TROOP_DATA = new Dictionary<string, GameUnitData>()
    //{
    //    {"Swordsman", new TroopData(
    //        "Swordsman",
    //        100,
    //        new CostValue("Food", 150),
    //        new CostValue("Gold", 20))},

    //    {"Catapult", new TroopData(
    //        "Catapult",
    //        250,
    //        new CostValue("Food", 100),
    //        new CostValue("Wood", 200),
    //        new CostValue("Gold", 100))}
    //};
}