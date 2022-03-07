using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluidHTN;
using FluidHTN.Compounds;
using FluidHTN.Contexts;
using FluidHTN.Factory;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; } // this

    public GameObject Domain;

    private Domain<AIContext> _domain;
    private Planner<AIContext> _planner;
    private AIContext _context;
    private AISenses _senses;

    private IList<BuildingSpot> _buildingSpots;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        _domain = Domain.GetComponent<AbstractDomain>().Domain;
        _planner = new Planner<AIContext>();
        _context = new AIContext();
        _context.Init();
        _senses = new AISenses(_context);

        _buildingSpots = new List<BuildingSpot>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        _senses.Tick();
        _planner.Tick(_domain, _context, false);
    }

    public void RegisterBuildingSpot(BuildingSpot buildingSpot)
    {
        _buildingSpots.Add(buildingSpot);
    }

    public BuildingSpot GetFreeBuildingSpot()
    {
        foreach (BuildingSpot buildingSpot in _buildingSpots)
        {
            if (!buildingSpot.Occupied) return buildingSpot;
        }
        return null;
    }

    // Build in random free building spot
    public TaskStatus BuildBuilding(string buildingType)
    {
        BuildingSpot buildingSpot = GetFreeBuildingSpot();
        if (!buildingSpot)
        {
            return TaskStatus.Failure;
        }

        Vector3 position = buildingSpot.Position;
        BuildingData buildingData = Globals.BUILDING_DATA[buildingType];
        Building building = new Building(buildingData);
        buildingSpot.AddBuilding(building);

        BuildingManager.Instance.BuildBuilding(building, position);
        return TaskStatus.Success;
    }

}
