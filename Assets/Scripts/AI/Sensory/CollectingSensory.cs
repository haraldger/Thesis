using System;
using System.Linq;
using FluidHTN;

public class CollectingSensory : ISensory
{

    public CollectingSensory(AIContext context)
    {
        this._context = context;
    }

    public void Tick()
    {
        SenseIdleWorkers();
    }

    //--------------------------------------- Internal Routines

    public void SenseIdleWorkers()
    {
        int idleWorkers = IdleWorkers();
        _context.SenseState(AIWorldState.IdleWorkers, idleWorkers);
    }

    private int IdleWorkers()
    {
        var workers = Globals.EXISTING_UNITS.Keys.Where(unit => unit.Data.code == "Worker").Cast<WorkerController>();
        int idleWorkers = workers.Where(worker => worker.CollectingTarget == null).Count();
        return idleWorkers;
    }

    //--------------------------------------- Public Sensory Getters

    public bool CanCollectResource(string resourceType)
    {
        if (AIManager.Instance.GetResourceSpotType(resourceType) == null) return false;
        if (IdleWorkers() <= 0) return false;

        return true;
    }


    //--------------------------------------- Private Fields

    private readonly AIContext _context;
}

