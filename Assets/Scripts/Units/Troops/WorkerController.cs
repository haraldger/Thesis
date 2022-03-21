using System;
using System.Collections;
using UnityEngine;

public class WorkerController : TroopController
{
    public WorkerData data;

    private GameResourceController _collectingTarget;

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

    public override void StopCommand()
    {
        base.StopCommand();
        _collectingTarget = null;
    }

    public void CollectResourceCommand(Transform target)
    {
        if (target == null) return;
        if (target.tag != "GameResource") return;

        GameResourceController targetController = target.gameObject.GetComponentInChildren<GameResourceController>();
        if (targetController == null) return;

        if (_collectingTarget == targetController) return; // Already collecting

        _collectingTarget = targetController;
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        _currentCoroutine = StartCoroutine(CollectResourceCoroutine());
    }

    private IEnumerator CollectResourceCoroutine()
    {
        while (_collectingTarget != null && _collectingTarget.amount > 0)
        {
            // Move within range of resource
            Move(_collectingTarget.transform);
            yield return new WaitUntil(() =>
            {
                float distance = Vector3.Distance(gameObject.transform.position, _collectingTarget.transform.position);
                return !IsMoving();
            });
            Stop();

            // Collect (with cooldown)
            _collectingTarget.Collect(data.collectionAmount);
            Globals.RESOURCE_DATA[_collectingTarget.code].AddResource(data.collectionAmount);
            yield return new WaitForSecondsRealtime(data.collectionSpeed);
        }
    }
}

