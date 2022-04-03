using System;
using FluidHTN;
using UnityEngine;

public class CollectingSensory : ISensory
{

    public CollectingSensory(AIContext context)
    {
        this._context = context;
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
        SetCanCollectGold();
        SetCanCollectWood();
        SetCanCollectFood();
    }

    /// <summary>
    /// This method updates the world state during planning. The changes to the
    /// world state are marked with the "PlanAndExecute" effect type, and will be
    /// undone during execution of a plan. Do not use this method to update the
    /// world state externally, i.e. outside of planning.
    /// </summary>
    public void UpdateWorldState()
    {
        UpdateCanCollect("Gold");
        UpdateCanCollect("Wood");
        UpdateCanCollect("Food");
    }

    //--------------------------------------- Internal Routines

    private void SetCanCollectGold()
    {
        _context.SetState(AIWorldState.CanCollectGold, CanCollectResource("Gold"), EffectType.Permanent);
    }

    private void SetCanCollectWood()
    {
        _context.SetState(AIWorldState.CanCollectWood, CanCollectResource("Wood"), EffectType.Permanent);
    }

    private void SetCanCollectFood()
    {
        _context.SetState(AIWorldState.CanCollectFood, CanCollectResource("Food"), EffectType.Permanent);
    }


    //--------------------------------------- Public Sensory Getters and Setters

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

    public void UpdateCanCollect(string resourceType)
    {
        if (CanCollectResource(resourceType))
        {
            switch (resourceType)
            {
                case "Gold":
                    _context.SetState(AIWorldState.CanCollectGold, true, EffectType.PlanAndExecute);
                    break;

                case "Wood":
                    _context.SetState(AIWorldState.CanCollectGold, true, EffectType.PlanAndExecute);
                    break;

                case "Food":
                    _context.SetState(AIWorldState.CanCollectFood, true, EffectType.PlanAndExecute);
                    break;

                default:
                    break;
            }
        }
    }


    //--------------------------------------- Private Fields

    private readonly AIContext _context;
}

