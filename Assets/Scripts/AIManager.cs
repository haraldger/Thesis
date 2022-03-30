using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
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


    private IList<BuildingSpot> _buildingSpots = new List<BuildingSpot>();
    private IList<ResourceSpot> _resourceSpots = new List<ResourceSpot>();
    private BuildingData[] _buildingData;
    private TroopData[] _troopData;


    void Awake()
    {
        Instance = this;
        _buildingData = Globals.BUILDING_DATA;
        _troopData = Globals.TROOP_DATA;
    }


    void Start()
    {
        _domain = Domain.GetComponent<AbstractDomain>().Domain;
        _planner = new Planner<AIContext>();
        _context = new AIContext();
        _context.Init();
        _senses = new AISenses(_context);

        // Player init on game startup
        GameObject[] initialObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject gameObject in initialObjects)
        {
            BuildingController building = gameObject.GetComponent<BuildingController>();
            if (building != null) _context.AddBuilding(building);
        }
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        _senses.Tick();
        _planner.Tick(_domain, _context, false);

        if (_context.LogDecomposition)
        {
            while (_context.DecompositionLog?.Count > 0)
            {
                var entry = _context.DecompositionLog.Dequeue();
                var depth = FluidHTN.Debug.Debug.DepthToString(entry.Depth);
                Debug.Log($"{depth}{entry.Name}: {entry.Description}");
            }
        }
    }




    // Build in random free building spot
    public TaskStatus BuildBuilding(string buildingType)
    {
        // Check building constraints
        if (!_senses.CanBuildBuilding(buildingType)) return TaskStatus.Failure;

        // Get available building spot
        BuildingSpot buildingSpot = GetFreeBuildingSpot();
        if (!buildingSpot)
        {
            return TaskStatus.Failure;
        }


        // Get data
        Vector3 position = buildingSpot.Position;
        BuildingData buildingData;

        try
        {
            buildingData = Array.Find(_buildingData, data => data.code == buildingType);
        }
        catch (ArgumentNullException)
        {
            return TaskStatus.Failure;
        }

        // Build
        BuildingController newBuilding;
        if(BuildingManager.Instance.BuildBuilding(buildingData, position, out newBuilding))
        {
            buildingSpot.AddBuilding(newBuilding);
            _context.AddBuilding(newBuilding);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
            
    }

    // Recruit troop from an available barracks
    public TaskStatus RecruitTroop(string unitType)
    {
        // Check recruiting constraints
        if (!_senses.CanRecruitTroop(unitType)) return TaskStatus.Failure;

        // Get recruiting barracks
        BuildingController barracks = _context.GetBuilding("Barracks");
        if (barracks == null) return TaskStatus.Failure;


        // Get Data
        TroopData troopData;
        try
        {
            troopData = Array.Find(_troopData, data => data.code == unitType);
        }
        catch (ArgumentNullException)
        {
            return TaskStatus.Failure;
        }


        // Recruit
        TroopController newTroop;
        if (barracks.RecruitTroop(troopData, out newTroop))
        {
            _context.AddTroop(newTroop);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }

    // Collect resources of the given type
    public TaskStatus CollectResource(string resourceType)
    {
        // Get Data
        Debug.Log("Collect resource called in AIManager");

        WorkerController worker = _context.GetIdleWorker();
        if (worker == null) return TaskStatus.Failure;

        ResourceSpot resourceSpot = GetResourceSpotType(resourceType);
        if (resourceSpot == null) return TaskStatus.Failure;

        // Issue collection command

        worker.CollectResourceCommand(resourceSpot.transform);
        return TaskStatus.Success;
    }



    //-------------------------------------- Utility methods

    // Invoked on resource spot init
    public void RegisterResourceSpot(ResourceSpot resourceSpot)
    {
        _resourceSpots.Add(resourceSpot);
    }

    // Invoked on resource spot destruction
    public void ConsumeResourceSpot(ResourceSpot resourceSpot)
    {
        _resourceSpots.Remove(resourceSpot);
    }

    // Gets a resource spot of the given type
    public ResourceSpot GetResourceSpotType(string resourceType)
    {
        return _resourceSpots.DefaultIfEmpty(null).Where(gameResource => gameResource.code == resourceType).OrderBy(gameResource => gameResource.Workers).FirstOrDefault();
    }

    // Invoked on building spot init
    public void RegisterBuildingSpot(BuildingSpot buildingSpot)
    {
        _buildingSpots.Add(buildingSpot);
    }

    // Get an available building spot
    public BuildingSpot GetFreeBuildingSpot()
    {
        foreach (BuildingSpot buildingSpot in _buildingSpots)
        {
            if (!buildingSpot.Occupied) return buildingSpot;
        }
        return null;
    }

}
