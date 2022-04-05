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
    private Domain<AIContext, int> _domain;
    private Planner<AIContext, int> _planner;
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
        _planner = new Planner<AIContext, int>();
        _context = new AIContext();
        _context.Init();
        _senses = new AISenses(_context);

        // Player init on game startup
        GameObject[] initialObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject gameObject in initialObjects)
        {
            UnitController unit = gameObject.GetComponent<UnitController>();
            if (unit == null) continue;

            if (unit is BuildingController building)
            {
                _context.AddBuilding(building);
            }
            else if (unit is TroopController troop)
            {
                _context.AddTroop(troop);
            }
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
    public TaskStatus RecruitTroop(string troopType)
    {
        // Check recruiting constraints
        if (!_senses.CanRecruitTroop(troopType)) return TaskStatus.Failure;

        // Get recruiting building
        BuildingController requiredBuilding;
        try
        {
            var requiredType = _buildingData.First(building => building.recruitingOptions.Where(availableTroop => availableTroop.code == troopType).Any()).code;

            requiredBuilding = _context.GetBuilding(requiredType);
            if (requiredBuilding == null)
            {
                return TaskStatus.Failure;
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Get recruiting building failure, no type {troopType} available as a recruiting option from any building");
            Debug.Log(ex.StackTrace);
            return TaskStatus.Failure;
        }


        // Get Data
        TroopData troopData;
        try
        {
            troopData = Array.Find(_troopData, data => data.code == troopType);
        }
        catch (ArgumentNullException ex)
        {
            Debug.Log($"Get data failure, no type {troopType} in _troopData");
            Debug.Log(ex.StackTrace);
            return TaskStatus.Failure;
        }


        // Recruit
        TroopController newTroop;
        if (requiredBuilding.RecruitTroop(troopData, out newTroop))
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
        // Initial condition checking
        if (!_senses.CanCollectResource(resourceType)) return TaskStatus.Failure;

        // Get Data

        WorkerController worker = _context.GetIdleWorker();
        ResourceSpot resourceSpot = GetResourceSpotType(resourceType);

        // Issue collection command

        worker.CollectResourceCommand(resourceSpot.transform);
        return TaskStatus.Success;
    }

    // Unassign a worker from collecting, making it idle
    public TaskStatus UnassignWorker()
    {
        var busyWorker = _context.GetBusyWorker();
        if (busyWorker == null) return TaskStatus.Failure;

        busyWorker.StopCommand();
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

    public List<BuildingSpot> GetFreeBuildingSpots()
    {
        return _buildingSpots.Where(buildingSpot => buildingSpot.Occupied == false).ToList();
    }

}
