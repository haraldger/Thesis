using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TroopController : UnitController
{
    public TroopData data;

    public Vector3 Goal { get; set; }

    private NavMeshAgent _navMesh;

    private UnitController _attackTarget;

    private bool _attacking;

    private Coroutine _attackingCoroutine;


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

    public override void Start()
    {
        base.Start();
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

        ///
        /// The following piece of code was taken from Unity forums
        /// Credit: http://answers.unity.com/answers/1699424/view.html
        /// User: unity_ek98vnTRplGj8Q
        /// 
        Vector3 playerDirection = gameObject.transform.position - destination.position;
        Vector3 adjustedDestination = destination.position + playerDirection.normalized;
        ///

        MoveTo(adjustedDestination);
    }

    public void MoveTo(Vector3 destination)
    {
        Goal = destination;
    }

    public void Stop()
    {
        Goal = gameObject.transform.position;
    }

    public void Attack(Transform target)
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
            MoveTo(_attackTarget.transform);
            yield return new WaitUntil(() =>
            {
                float distance = Vector3.Distance(gameObject.transform.position, _attackTarget.transform.position);
                return distance < data.attackRange;
            });
            Stop();

            // Damage target (with cooldown)
            _attackTarget.Damage(data.attackPower);
            yield return new WaitForSecondsRealtime(data.attackSpeed);
        }

        Debug.Log("Enemy HP at 0!");
    }

}

