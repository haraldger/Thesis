using System.Collections.Generic;

public class Globals 
{
    public static IDictionary<string, GameResourceData> RESOURCE_DATA = new Dictionary<string, GameResourceData>()
    {
        {"Wood", new GameResourceData("Wood", 2000, 200)},
        {"Gold", new GameResourceData("Gold", 2000, 50)},
        {"Food", new GameResourceData("Food", 2000, 250)}
    };

    public static IDictionary<string, GameUnitData> BUILDING_DATA = new Dictionary<string, GameUnitData>()
    {
        {"Barracks", new GameUnitData("Barracks", 100, new Dictionary<GameResourceData, int>(){
            {RESOURCE_DATA["Wood"], 200},
            {RESOURCE_DATA["Gold"], 50}
        })},

        {"Citadel", new GameUnitData("Citadel", 1000, new Dictionary<GameResourceData, int>(){
            {RESOURCE_DATA["Wood"], 1000},
            {RESOURCE_DATA["Gold"], 500}
        })},

        {"Farm", new GameUnitData("Farm", 75, new Dictionary<GameResourceData, int>(){
            {RESOURCE_DATA["Wood"], 100}
        })}
    };

    //public static IDictionary<BuildingData, IDictionary<GameResourceData, int>> BUILDING_COSTS = new Dictionary<BuildingData, IDictionary<GameResourceData, int>>()
    //{

    //};
}