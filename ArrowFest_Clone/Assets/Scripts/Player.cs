using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int arrowCount;
    [SerializeField] private ArrowSystem arrowSystem;

    private List<Transform> _arrows = new List<Transform>();
    private CounterDisplay _counterDisplay;
    private ScreenLoader _screenLoader;
    private SplineFollower _follower;
    
    public bool isAllive = true;
    public List<Transform> Arrows { get => _arrows; }
    public int ArrowCount { get => arrowCount; }

    private void Awake()
    {
        _counterDisplay = GameManager.Instance.scoreBoard.GetComponent<CounterDisplay>();
        _screenLoader = GameManager.Instance.gameStates.GetComponent<ScreenLoader>();
        _follower = GetComponent<SplineFollower>();
    }

    // Start is called before the first frame update
    void Start()
    {
        arrowSystem.AddArrows(arrowCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _screenLoader.AnimateLoseScreen();
    }

    public void Move(Vector2 target, float leftLimit, float rightLimit, float speed)
    {
        _follower.motion.offset += new Vector2(target.x * speed / 1000, 0);

        if (_follower.motion.offset.x > rightLimit || _follower.motion.offset.x < leftLimit)
            _follower.motion.offset = new Vector2(Mathf.Clamp(_follower.motion.offset.x, leftLimit, rightLimit), _follower.motion.offset.y);
    }

    public void TakeDamage(GameObject other)
    {
        var enemy = other.GetComponent<Enemy>();
        arrowSystem.RemoveArrows(enemy.damagePower);
    }

    public void Dead()
    {
        isAllive = false;
        _follower.follow = false;
        _screenLoader.AnimateLoseScreen();
    }
    public void UpdateScoreBoard()
    {
        arrowCount = _arrows.Count;
        _counterDisplay.UpdateCounter(arrowCount);
        _counterDisplay.AnimateScoreBoard();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(gameObject);
            TakeDamage(other.gameObject);
        }else if(other.gameObject.GetComponent<IBooster>() != null)
        {
            other.gameObject.GetComponent<IBooster>().Boost(gameObject);
        }
    }
}
