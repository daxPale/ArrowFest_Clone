using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Position
    {
        Right,
        Middle,
        Left
    };

    [SerializeField] private int _damagePower;
    [SerializeField] private Position _position;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        player.RemoveArrows(_damagePower);
    }

    private void SetPosition()
    {
        float offset = _position == Position.Right ? 0.25f
                     : _position == Position.Left ? -0.25f
                     : 0;

        transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
