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

    private Domain<AIContext> _domain;
    private Planner<AIContext> _planner;
    private AIContext _context;
    private AISenses _senses;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        _domain = SimpleBuildDomain.Domain;
        _planner = new Planner<AIContext>();
        _context = new AIContext();
        _context.Init();
        _senses = new AISenses(_context);

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

    public void BuildBuilding(string buildingType)
    {
        BuildingData building = Globals.BUILDING_DATA[buildingType];
        Vector3 position = new Vector3(5.0f, 5.0f, 5.0f);

        BuildingManager.Instance.BuildBuilding(building, position);
    }
}
