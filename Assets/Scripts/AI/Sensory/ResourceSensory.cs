using System;
using System.Collections.Generic;

public class ResourceSensory : ISensory
{

    public ResourceSensory(AIContext context)
    {
        _context = context;
        _resourceData = Globals.RESOURCE_DATA;
    }

    public void Tick()
    {
        SenseResourceAmounts();
    }


    //--------------------------------------- Internal routines

    private void SenseResourceAmounts()
    {
        foreach (var resource in _resourceData)
        {
            _context.SenseResource(resource.Key, resource.Value.CurrentAmount);
        }
    }


    //--------------------------------------- Private Fields

    private readonly AIContext _context;
    private readonly IDictionary<string, GameResourceData> _resourceData;
}

