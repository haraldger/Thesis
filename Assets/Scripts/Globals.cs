using System.Collections.Generic;

public class Globals 
{

    public static IDictionary<string, BuildingData> BUILDING_DATA = new Dictionary<string, BuildingData>()
    {
        {"Barracks", new BuildingData("Barracks", 100)},
        {"Citadel", new BuildingData("Citadel", 1000)}
    };

    //public static BuildingData[] BUILDING_DATA = new BuildingData[]
    //{
    //    new BuildingData("Barracks", 100),
    //    new BuildingData("Citadel", 1000)
    //};


}