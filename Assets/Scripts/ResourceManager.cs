using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }


    public int ResourceCap { get; private set; }

    public int Food { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ResourceCap = 10000;
        Food = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseFood(int amount)
    {
        if (Food + amount <= ResourceCap)
        {
            Food += amount;
        }
        else
        {
            Food = ResourceCap;
        }
    }

    public void consumeFood(int amount)
    {
        if (Food - amount > 0)
        {
            Food -= amount;
        }
        else
        {
            Food = 0;
        }
    }
}
