using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController: MonoBehaviour
{
    public bool Selected { get; set; }

    public virtual void Awake()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
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

}
