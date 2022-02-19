using System;
public class BuildingData
{
    public string Code { get; private set; }

    public int HP { get; private set;  }

    public BuildingData(string code, int hp)
    {
        Code = code;
        HP = hp; 
    }
}
