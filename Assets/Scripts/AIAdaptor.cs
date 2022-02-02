using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAdaptor : MonoBehaviour
{
    public void BuildBarracks(Vector3 location)
    {
        for (int i = 0; i < Globals.BUILDING_DATA.Length; i++)
        {
            if (Globals.BUILDING_DATA[i].Code == "Barracks")
            {
                BuildingManager.Instance.BuildBuilding(Globals.BUILDING_DATA[i], location);
                break;
            }
        }
    }
}
