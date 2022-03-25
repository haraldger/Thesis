using System.Collections;
using System.Collections.Generic;
using System;
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


    void Awake()
    {
        Instance = this;
        _buildingSpots = new List<BuildingSpot>();
    }

    void Start()
    {
        _domain = Domain.GetComponent<AbstractDomain>().Domain;
        _planner = new Planner<AIContext>();
        _context = new AIContext();
        _context.Init();
        _senses = new AISenses(_context);
    }

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
        // Get available building spot
        BuildingSpot buildingSpot = GetFreeBuildingSpot();
        if (!buildingSpot)
        {
            return TaskStatus.Failure;
        }

        
        Vector3 position = buildingSpot.Position;
        BuildingData buildingData = Array.Find(Globals.BUILDING_DATA, data => data.code == buildingType);

        BuildingController newBuilding;
        if(BuildingManager.Instance.BuildBuilding(buildingData, position, out newBuilding))
        {
            buildingSpot.AddBuilding(newBuilding);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
            
    }

}
