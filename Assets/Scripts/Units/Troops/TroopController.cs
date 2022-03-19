using System;
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

        // Data init
        MaxHP = data.hp;
        CurrentHP = MaxHP;

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
        // Don't interact with self
        if (destination == gameObject.transform) return;

        MoveTo(destination.position);
    }

    public void MoveTo(Vector3 destination)
    {
        Debug.Log("Moving!");
        Goal = destination;
    }

    public void Attack(Transform target)
    {
        // Don't interact with self
        if (target == gameObject.transform) return;

        Debug.Log("Attacking!");
        MoveTo(target);
    }

}

