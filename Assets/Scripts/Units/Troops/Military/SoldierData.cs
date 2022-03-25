using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Soldier", menuName = "Scriptable Objects/Soldier", order = 2)]
public class SoldierData : TroopData
{
    public int attackPower;

    public float attackRange;

    public float attackSpeed;
}

