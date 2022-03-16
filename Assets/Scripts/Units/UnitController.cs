using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController: MonoBehaviour
{
    public bool Selected { get; set; }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
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
