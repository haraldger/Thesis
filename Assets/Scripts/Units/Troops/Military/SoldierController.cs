using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SoldierController : TroopController
{
    public SoldierData data;

    private UnitController _attackTarget;

    private Coroutine _attackingCoroutine;

    public override void Awake()
    {
        base.Awake();

        // Data init
        MaxHP = data.hp;
        CurrentHP = MaxHP;

    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }



    public void AttackCommand(Transform target)
    {
        if (target == null) return; 
        if (target == gameObject.transform) return; // Don't interact with self

        UnitController targetController = target.gameObject.GetComponentInChildren<UnitController>();
        if (targetController == null) return;
        if (_attackTarget == targetController) return; // Already attacking

        _attackTarget = targetController;
        if (_attackingCoroutine != null) StopCoroutine(_attackingCoroutine);  // Stop current coroutine
        _attackingCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (_attackTarget != null && _attackTarget.CurrentHP > 0)
        {
            // Move withing range of target
            Move(_attackTarget.transform);
            yield return new WaitUntil(() =>
            {
                float distance = Vector3.Distance(gameObject.transform.position, _attackTarget.transform.position);
                return distance < data.attackRange;
            });
            Stop();

            // Damage target (with cooldown)
            if (data.attackPower > 0)
            {
                _attackTarget.Damage(data.attackPower);
                yield return new WaitForSecondsRealtime(data.attackSpeed);
            }
        }
    }

}

