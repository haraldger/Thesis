using System;
using UnityEngine;

public class WorkerController : TroopController
{
    public WorkerData data;

    public override void Awake()
    {
        base.Awake();

        // Data init
        MaxHP = data.hp;
        CurrentHP = MaxHP;

    }

    public override void Update()
    {
        base.Update();
    }



    public void CollectResourceCommand(Transform target)
    {
        if (target == null) return;
        if (target.tag != "GameResource") return;

    }
}

