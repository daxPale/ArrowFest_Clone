using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float offset = position == Position.Right ? 0.25f
                     : position == Position.Left ? -0.25f
                     : 0;

        transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
    }

    public void TakeDamage(GameObject other)
    {
        var player = other.GetComponent<Player>();
        if (player.ArrowCount > damagePower)
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }
}
