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
    private bool _isAllive = true;

    public List<Transform> Arrows { get => _arrows; }
    public int ArrowCount { get => arrowCount; }
    public bool IsAllive { get => _isAllive; }

    private void Awake()
    {
        _counterDisplay = GameManager.Instance.scoreBoard.GetComponent<CounterDisplay>();
        _screenLoader = GameManager.Instance.gameStates.GetComponent<ScreenLoader>();
        _follower = GetComponent<SplineFollower>();
        _follower.onNode += OnNodePassed;
    }

    // Start is called before the first frame update
    void Start()
    {
        arrowSystem.AddArrows(arrowCount);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Move(Vector2 target, float leftLimit, float rightLimit, float speed)
    {
        //In order to move the arrows, we need to change offset values of spline follower component
        _follower.motion.offset += new Vector2(target.x * Time.deltaTime * speed / 50, 0);

        //If the limit is exceeded, these values ​​are clamped.
        if (_follower.motion.offset.x > rightLimit || _follower.motion.offset.x < leftLimit)
            _follower.motion.offset = new Vector2(Mathf.Clamp(_follower.motion.offset.x, leftLimit, rightLimit), _follower.motion.offset.y);
    }
    public void Follow()
    {
        GameManager.Instance.IsPlaying = true;
        _follower.follow = true;
        _screenLoader.AnimateFirstTap();
    }

    public void Dead()
    {
        GameManager.Instance.IsPlaying = false;
        _isAllive = false;
        _follower.follow = false;
        _screenLoader.AnimateLoseScreen();
    }

    public void TakeDamage(GameObject other)
    {
        //Remove arrows by the amount of enemy damage
        var enemy = other.GetComponent<Enemy>();
        arrowSystem.RemoveArrows(enemy.damagePower);
    }

    public void UpdateScoreBoard()
    {
        arrowCount = _arrows.Count;
        _counterDisplay.UpdateCounter(arrowCount);
        _counterDisplay.AnimateScoreBoard();
    }

    private void OnNodePassed(List<SplineTracer.NodeConnection> passed)
    {
        //Check if the player has reached the end node,
        //If it had, show the win screen
        SplineTracer.NodeConnection nodeConnection = passed[0];
        if (nodeConnection.node.name == "Node_1")
        {
            GameManager.Instance.IsPlaying = false;
            _isAllive = false;
            _screenLoader.AnimateWinScreen();
        }
    }

    private void OnTriggerEnter(Collider other)
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
