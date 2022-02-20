using System.Collections.Generic;

public class Globals 
{

    public static IDictionary<string, BuildingData> BUILDING_DATA = new Dictionary<string, BuildingData>()
    {
        {"Barracks", new BuildingData("Barracks", 100)},
        {"Citadel", new BuildingData("Citadel", 1000)}
    };

    public static IDictionary<string, GameResourceData> RESOURCE_DATA = new Dictionary<string, GameResourceData>()
    {
        {"Wood", new GameResourceData("Wood", 2000, 100)},
        {"Gold", new GameResourceData("Gold", 2000, 0)},
        {"Food", new GameResourceData("Food", 2000, 250)}
    };
}