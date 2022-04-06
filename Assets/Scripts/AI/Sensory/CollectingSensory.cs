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
        SetCanCollectFood();
        SetCanCollectWood();
        SetCanCollectGold();
    }

    //--------------------------------------- Internal Routines

    public void SetCanCollectGold()
    {
        _context.SetState(AIWorldState.CanCollectGold, CanCollectResource("Gold"));
    }

    public void SetCanCollectWood()
    {
        _context.SetState(AIWorldState.CanCollectWood, CanCollectResource("Wood"));
    }

    public void SetCanCollectFood()
    {
        _context.SetState(AIWorldState.CanCollectFood, CanCollectResource("Food"));
    }

    //--------------------------------------- Public Sensory Getters

    public bool CanCollectResource(string resourceType)
    {
        if (AIManager.Instance.GetResourceSpotType(resourceType) == null) return false;

        return true;
    }


    //--------------------------------------- Private Fields

    private readonly AIContext _context;
}