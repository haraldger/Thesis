using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAdaptor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BuildBarracks(Vector3 buildingLocation)
    {
        BuildingData barracksData = Globals.BUILDING_DATA["Barracks"];
        BuildingManager.Instance.BuildBuilding(barracksData, buildingLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
