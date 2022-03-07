using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    public Vector3 Position { get => gameObject.transform.position; }
    public bool Occupied { get; private set; } = false;
    public Building Building { get; private set; } = null;

    // Start is called before the first frame update
    void Start()
    {
        AIManager.Instance.RegisterBuildingSpot(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Building != null && Building.Instance == null)
        {
            RemoveBuilding();
        }
    }

    public void AddBuilding(Building building)
    {
        Occupied = true;
        Building = building;
    }

    public void RemoveBuilding()
    {
        Occupied = false;
        Building = null;
    }
}
