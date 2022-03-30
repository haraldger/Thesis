using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController: MonoBehaviour
{
    public virtual GameUnitData Data { get; protected set; }

    public bool Selected { get; set; }

    public int MaxHP { get; protected set; }

    private int _currentHP;
    public int CurrentHP
    {
        get => _currentHP;
        protected set
        {
            if (value < 0)
            {
                _currentHP = 0;
            }
            else if (value > MaxHP)
            {
                _currentHP = MaxHP;
            }
            else
            {
                _currentHP = value;
            }
        }
    }

    public Animator animator;


    public virtual void Awake()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if(CurrentHP <= 0)
        {
            GameManager.Instance.DestroyUnit(this);
            enabled = false;
        }
    }


    public void Select()
    {
        Selected = true;
        gameObject.GetComponentInChildren<LineRenderer>().enabled = true;
    }

    public void Deselect()
    {
        Selected = false;
        gameObject.GetComponentInChildren<LineRenderer>().enabled = false;
    }

    public void Damage(int amount)
    {
        CurrentHP -= amount;
        animator.SetTrigger("Hurt");
    }

    public void Heal(int amount)
    {
        CurrentHP += amount;
    }

}
