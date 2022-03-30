using System;
using System.Collections;
using UnityEngine;

public class WorkerController : TroopController
{
    public WorkerData data;

    public override GameUnitData Data { get => data; protected set => base.Data = value; }

    public GameResourceController CollectingTarget { get; private set; }

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
        CollectingTarget = null;
    }

    public void CollectResourceCommand(Transform target)
    {
        if (target == null) return;

        GameResourceController targetController = target.gameObject.GetComponentInChildren<GameResourceController>();
        if (targetController == null) return;

        if (CollectingTarget == targetController) return; // Already collecting

        StopCommand();
        CollectingTarget = targetController;
        _currentCoroutine = StartCoroutine(CollectResourceCoroutine());
    }

    private IEnumerator CollectResourceCoroutine()
    {
        while (CollectingTarget != null && CollectingTarget.amount > 0)
        {
            // Move within range of resource
            Move(CollectingTarget.transform);
            yield return new WaitUntil(() =>
            {
                float distance = Vector3.Distance(gameObject.transform.position, CollectingTarget.transform.position);
                return !IsMoving();
            });
            Stop();

            // Collect (with cooldown)
            CollectingTarget.Collect(data.collectionAmount);
            Globals.RESOURCE_DATA[CollectingTarget.code].AddResource(data.collectionAmount);
            yield return new WaitForSecondsRealtime(data.collectionSpeed);
        }
    }

}

