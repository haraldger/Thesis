using FluidHTN;
using UnityEngine;

public abstract class AbstractDomain : MonoBehaviour
{
    public abstract Domain<AIContext, int> Domain { get; set; }
}
