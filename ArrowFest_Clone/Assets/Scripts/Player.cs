using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int arrowCount;
    [SerializeField] private ArrowSystem arrowSystem;

    private List<Transform> _arrows = new List<Transform>();
    private CounterDisplay _counterDisplay;
    public bool isAllive = true;
    public List<Transform> Arrows { get => _arrows; }
    public int ArrowCount { get => arrowCount; }

    private void Awake()
    {
        _counterDisplay = GameManager.Instance.scoreBoard.GetComponentInChildren<CounterDisplay>();
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
            arrowSystem.AddArrows(1);
        if (Input.GetKeyDown(KeyCode.C))
            arrowSystem.RemoveArrows(1);
    }

    public void TakeDamage(GameObject other)
    {
        var enemy = other.GetComponent<Enemy>();
        arrowSystem.RemoveArrows(enemy.damagePower);
    }
    public void Dead()
    {
        isAllive = false;
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

    public void UpdateScoreBoard()
    {
        arrowCount = _arrows.Count;
        _counterDisplay.UpdateCounter(arrowCount);
        _counterDisplay.AnimateScoreBoard();
    }
}
