using System;
using UnityEngine;

public class CollectingSensory : ISensory
{

    public CollectingSensory(AIContext context)
    {
        this._context = context;
    }

    public void Tick()
    {
        SetCanCollectGold();
        SetCanCollectWood();
        SetCanCollectFood();
    }

    //--------------------------------------- Internal Routines

    private void SetCanCollectGold()
    {
        _context.SetState(AIWorldState.CanCollectGold, CanCollectResource("Gold"));
    }

    private void SetCanCollectWood()
    {
        _context.SetState(AIWorldState.CanCollectWood, CanCollectResource("Wood"));
    }

    private void SetCanCollectFood()
    {
        _context.SetState(AIWorldState.CanCollectFood, CanCollectResource("Food"));
    }


    //--------------------------------------- Public Sensory Getters

    public bool CanCollectResource(string resourceType)
    {
        // Check for available worker
        if (_context.GetIdleWorker() == null)
        {
            return false;
        }

        // Check for available resource spot
        if (AIManager.Instance.GetResourceSpotType(resourceType) == null)
        {
            return false;
        }

        return true;
    }


    //--------------------------------------- Private Fields

    private readonly AIContext _context;
}

