using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public enum Position
    {
        Right,
        Middle,
        Left
    };

    public int damagePower;
    public Position position;

    public abstract void SetPosition();
}
