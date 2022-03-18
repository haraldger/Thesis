﻿using System;
using UnityEngine;
using UnityEngine.AI;

public class TroopController : UnitController
{
    public TroopData data;

    public Vector3 Goal { get; set; }

    private NavMeshAgent _navMesh;

    public override void Awake()
    {
        base.Awake();

        // AI NavMesh init
        _navMesh = gameObject.GetComponent<NavMeshAgent>();
        Goal = gameObject.transform.position;
    }

    public override void Update()
    {
        base.Update();

        // Update AI NavMesh movement
        _navMesh.destination = Goal;
    }

    public void MoveTo(Transform destination)
    {
        MoveTo(destination.position);
    }

    public void MoveTo(Vector3 destination)
    {
        Goal = destination;
    }

}

