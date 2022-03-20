using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TroopController : UnitController
{
    public TroopData data;

    private NavMeshAgent _agent;

    private NavMeshObstacle _obstacle;

    private Vector3 _goal;

    private float _stoppingDistance;

    private UnitController _attackTarget;

    private Coroutine _attackingCoroutine;




    public override void Awake()
    {
        base.Awake();

        // Data init
        MaxHP = data.hp;
        CurrentHP = MaxHP;

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

        if (Vector3.Distance(transform.position, _goal) < _stoppingDistance)
        {
            if (_obstacle.enabled == false)
            {
                Debug.Log("Obstacle");
                MakeObstacle();
            }
        }
        else
        {
            if (_agent.enabled == false)
            {
                Debug.Log("Agent");
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

    private void Move(Transform destination)
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

    private void Move(Vector3 destination)
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

    private void Stop()
    {
        MakeAgent();
        _agent.destination = gameObject.transform.position;
        MakeObstacle();
    }

    public void AttackCommand(Transform target)
    {
        if (target == null) return; 
        if (target == gameObject.transform) return; // Don't interact with self

        UnitController targetController = target.gameObject.GetComponentInChildren<UnitController>();
        if (_attackTarget == targetController) return; // Already attacking

        _attackTarget = targetController;
        if (_attackingCoroutine != null) StopCoroutine(_attackingCoroutine);  // Stop current coroutine
        _attackingCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (_attackTarget.CurrentHP > 0)
        {

            // Move withing range of target
            Move(_attackTarget.transform);
            yield return new WaitUntil(() =>
            {
                float distance = Vector3.Distance(gameObject.transform.position, _attackTarget.transform.position);
                return distance < data.attackRange;
            });
            StopCommand();

            // Damage target (with cooldown)
            _attackTarget.Damage(data.attackPower);
            yield return new WaitForSecondsRealtime(data.attackSpeed);
        }
    }

    // Enable NavMesh Agent
    private void MakeAgent()
    {
        _obstacle.enabled = false;
        _agent.enabled = true;
    }

    // Enable NavMesh Obstacle
    private void MakeObstacle()
    {
        _agent.enabled = false;
        _obstacle.enabled = true;
    }

}

