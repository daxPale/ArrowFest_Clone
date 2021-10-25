using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public abstract void TakeDamage(GameObject other);
}

public interface IBooster
{
    public abstract void Boost(GameObject other);
}

