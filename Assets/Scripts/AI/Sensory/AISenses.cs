using System;
using System.Collections.Generic;

public class AISenses
{

    public AISenses(AIContext context)
    {
        this._context = context;
        this._senses = new Dictionary<string, ISensory>();
        _senses["Building"] = new BuildingSensory(_context);
        _senses["Recruiting"] = new RecruitingSensory(_context);
        _senses["Collecting"] = new CollectingSensory(_context);
    }

    /// <summary>
    /// This method sets the world state permanently. That means that once set,
    /// the effects will not be removed. As such, this method should not be used
    /// during planning, but to update the world state externally. It's intended
    /// to facilitate the passing of time, where in-game event may alter the
    /// world state.
    /// </summary>
    public void Tick()
    {
        foreach (ISensory sensory in _senses.Values)
        {
            sensory.Tick();
        }
    }

    /// <summary>
    /// This method updates the world state during planning. The changes to the
    /// world state are marked with the "PlanAndExecute" effect type, and will be
    /// undone during execution of a plan. Do not use this method to update the
    /// world state externally, i.e. outside of planning.
    /// </summary>
    public void UpdateWorldState()
    {
        foreach (ISensory sensory in _senses.Values)
        {
            sensory.UpdateWorldState();
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

    public bool CanCollectResource(string resourceType)
    {
        CollectingSensory sensory = (CollectingSensory)_senses["Collecting"];
        return sensory.CanCollectResource(resourceType);
    }

    private readonly AIContext _context;
    private readonly IDictionary<string, ISensory> _senses;
}

