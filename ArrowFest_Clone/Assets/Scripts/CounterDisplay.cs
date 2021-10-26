using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CounterDisplay : MonoBehaviour
{
    private Image _scoreBoard;
    private Text _counter;

    private void Awake()
    {
        _scoreBoard = GetComponent<Image>();
        _counter = GetComponentInChildren<Text>();
    }

    public void UpdateCounter(int arrows)
    {
        _counter.text = arrows.ToString();
    }

    public void AnimateScoreBoard()
    {
        _scoreBoard.transform.transform.DORewind();
        _scoreBoard.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), .25f);
    }
}
