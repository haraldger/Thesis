using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanConsumeResource(GameResourceData gameResource, int amount)
    {
        if (gameResource.CurrentAmount - amount >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
