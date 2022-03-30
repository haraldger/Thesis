﻿using System;
using System.Collections.Generic;

public class AISenses
{

    public AISenses(AIContext context)
    {
        this._context = context;
        this._senses = new Dictionary<string, ISensory>();
        _senses["Building"] = new BuildingSensory(_context);
        _senses["Recruiting"] = new RecruitingSensory(_context);
    }

    public void Tick()
    {
        foreach (ISensory sensory in _senses.Values)
        {
            sensory.Tick();
        }
    }

    public bool CanBuildBuilding(string buildingType)
    {
        BuildingSensory sensory = (BuildingSensory)_senses["Building"];
        return sensory.CanBuildBuilding(buildingType);
    }

    public bool CanRecruitTroop(string troopType)
    {
        RecruitingSensory sensory = (RecruitingSensory)_senses["Recruiting"];
        return sensory.CanRecruitTroop(troopType);
    }

    private readonly AIContext _context;
    private readonly IDictionary<string, ISensory> _senses;
}

