using System;

[System.Serializable]
public struct CostValue
{
    public string code;

    public int value;

    public CostValue(string code, int value)
    {
        this.code = code;
        this.value = value;
    }
}

