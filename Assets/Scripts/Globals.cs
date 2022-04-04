using System.Collections.Generic;

public class Globals 
{
    public static IDictionary<string, GameResourceData> RESOURCE_DATA = new Dictionary<string, GameResourceData>()
    {
        {"Wood", new GameResourceData("Wood", 2000, 0)},
        {"Gold", new GameResourceData("Gold", 2000, 0)},
        {"Food", new GameResourceData("Food", 2000, 100)}
    };

    public static BuildingData[] BUILDING_DATA;

    public static TroopData[] TROOP_DATA;

    public static IDictionary<UnitController, GameUnit> EXISTING_UNITS = new Dictionary<UnitController, GameUnit>();
}