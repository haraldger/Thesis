﻿using System;
using UnityEngine;
using UnityEngine.AI;

public class TroopController : UnitController
{
    private NavMeshAgent _agent;

    private NavMeshObstacle _obstacle;

    private Vector3 _goal;

    private float _stoppingDistance;

    public override void Awake()
    {
        base.Awake();

        // AI NavMesh init
        _obstacle = gameObject.GetComponent<NavMeshObstacle>();
        _agent = gameObject.GetComponent<NavMeshAgent>();

        MakeAgent();
        _agent.enabled = true;
        _stoppingDistance = _agent.stoppingDistance;
        MakeObstacle();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (_goal == Vector3.negativeInfinity || Vector3.Distance(transform.position, _goal) < _stoppingDistance)
        {
            Stop();
            if (_obstacle.enabled == false)
            {
                MakeObstacle();
            }
        }
        else
        {
            if (_agent.enabled == false)
            {
                MakeAgent();
            }
        }
    }



    // Wrapper to cancel coroutines
    public void MoveCommand(Transform destination)
    {
        // Don't interact with self
        if (destination == gameObject.transform) return;

        StopAllCoroutines();
        Move(destination);
    }

    // Wrapper to cancel coroutines
    public void MoveCommand(Vector3 destination)
    {
        StopAllCoroutines();
        Move(destination);
    }

    protected void Move(Transform destination)
    {
        // Don't interact with self
        if (destination == gameObject.transform) return;

        ///
        /// The following piece of code was taken from Unity forums
        /// Credit: http://answers.unity.com/answers/1699424/view.html
        /// User: unity_ek98vnTRplGj8Q
        /// 
        Vector3 playerDirection = gameObject.transform.position - destination.position;
        Vector3 adjustedDestination = destination.position + playerDirection.normalized;
        ///

        Move(adjustedDestination);
    }

    protected void Move(Vector3 destination)
    {
        MakeAgent();
        _goal = destination;
        _agent.destination = destination;
    }

    // Wrapper to cancel coroutines
    public void StopCommand()
    {
        StopAllCoroutines();
        Stop();
    }

    protected void Stop()
    {
        MakeAgent();
        _agent.destination = Vector3.negativeInfinity;
        MakeObstacle();
    }


    // Enable NavMesh Agent
    protected void MakeAgent()
    {
        _obstacle.enabled = false;
        _agent.enabled = true;
    }

    // Enable NavMesh Obstacle
    protected void MakeObstacle()
    {
        _agent.enabled = false;
        _obstacle.enabled = true;
    }
}

