using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourceController : MonoBehaviour
{
    public string code;

    public int amount;

    public Animator animator;

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        if (this.amount <= 0)
        {
            Destroy(gameObject);
            enabled = false;
        }
    }

    public void Collect(int amount)
    {
        this.amount -= amount;
        animator.SetTrigger("Collect");
        Debug.Log("Collected, trigger 'Collect' set");
    }
}
