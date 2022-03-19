using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Troop", menuName = "Scriptable Objects/Troop", order = 2)]
public class TroopData : GameUnitData
{
    public int attackPower;

    public float attackRange;

    public float attackSpeed;
}

