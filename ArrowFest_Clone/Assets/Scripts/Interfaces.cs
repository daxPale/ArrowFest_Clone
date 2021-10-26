using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public abstract void TakeDamage(GameObject other);
}

public interface IBooster
{
    public enum Operator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    };

    public abstract void Boost(GameObject other);
}

