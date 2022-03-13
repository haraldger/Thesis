using System;
public struct CostValue
{
    public string Code { get; private set; }

    public int Value { get; private set; }

    public CostValue(string code, int value)
    {
        Code = code;
        Value = value;
    }
}

