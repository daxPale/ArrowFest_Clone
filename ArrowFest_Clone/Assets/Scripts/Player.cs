using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int _arrowCount;

    private List<Transform> _arrows = new List<Transform>();
    private CounterDisplay _counterDisplay;

    public List<Transform> Arrows { get => _arrows; }
    public int ArrowCount { get => _arrowCount; }

    public void AddArrows(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform ins = Instantiate(GameManager.Instance.arrowPrefab, transform.Find("Arrows"));
            _arrows.Add(ins);
        }
        _counterDisplay.AnimateCounterDisplay();
        UpdateCounter();
    }
    public void RemoveArrows(int count)
    {
        if(ArrowCount - count <= 0)
        {
            //game over
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Destroy(_arrows[ArrowCount - i - 1].gameObject);
            _arrows.RemoveAt(ArrowCount - i - 1);
        }
        _counterDisplay.AnimateCounterDisplay();
        UpdateCounter();
    }
    public void UpdateCounter()
    {
        _arrowCount = _arrows.Count;
        GameManager.Instance.scoreBoard.GetComponentInChildren<Text>().text = _arrowCount.ToString();
    }

    private void Awake()
    {
        _counterDisplay = GameManager.Instance.scoreBoard.GetComponentInChildren<CounterDisplay>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AddArrows(_arrowCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
