using FluidHTN;
using UnityEngine;

public abstract class AbstractDomain : MonoBehaviour
{
    public abstract Domain<AIContext> Domain { get; set; }
}
