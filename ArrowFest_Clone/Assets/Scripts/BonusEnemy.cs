using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BonusEnemy : Enemy, IDamageable
{
    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
    }

    public override void SetPosition()
    {
        float offset = position == Position.Right ? 0.60f
                     : position == Position.Left ? -0.60f
                     : 0;

        transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
    }

    public void TakeDamage(GameObject other)
    {
        var player = other.GetComponent<Player>();
        if (player.ArrowCount > damagePower)
        {
            transform.transform.DORewind();
            GetComponent<Renderer>().material.DOColor(Color.red, 0.1f);
        }
    }
}
