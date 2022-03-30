using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourceController : MonoBehaviour
{
    public string code;

    public int amount;

    public Animator animator;

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        if (this.amount <= 0)
        {
            Destroy(gameObject);
            enabled = false;
        }
    }

    public virtual void OnDestroy()
    {

    }


    public void Collect(int amount)
    {
        this.amount -= amount;
        animator.SetTrigger("Collect");
    }
}
