using System;
using System.Collections.Generic;

public class AISenses
{

    public AISenses(AIContext context)
    {
        this._context = context;
        this._senses = new List<ISensory>();
        _senses.Add(new BuildingSensory(_context));
    }

    public void Tick()
    {
        foreach (ISensory sensory in _senses)
        {
            sensory.Tick();
        }
    }

    private readonly AIContext _context;
    private readonly IList<ISensory> _senses;
}

