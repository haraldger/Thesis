using System.Collections.Generic;

public class Globals 
{

    public static IDictionary<string, BuildingData> BUILDING_DATA = new Dictionary<string, BuildingData>()
    {
        {"Barracks", new BuildingData("Barracks", 100)}
    };

}