using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicEnemy : Enemy, IDamageable
{
    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void SetPosition()
    {
        //Position enemies according to position value (right & left & middle)
        float offset = position == Position.Right ? 0.25f
                     : position == Position.Left ? -0.25f
                     : 0;

        transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
    }

    public void TakeDamage(GameObject other)
    {
        //Kill enemy if player has enough arrows (greater than damage power of enemy)
        var player = other.GetComponent<Player>();
        if (player.ArrowCount > damagePower)
        {
            transform.transform.DORewind();
            GetComponent<Renderer>().material.DOColor(Color.red, 0.1f);
        }
    }
}
