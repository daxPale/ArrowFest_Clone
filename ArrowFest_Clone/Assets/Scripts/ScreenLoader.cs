using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField] private Image _loseState;
    [SerializeField] private Image _winState;
    [Range(0, 1)]
    public float targetAlphaValue = 0.5f;
    [Range(0, 5)]
    public float fadeOutLenght = 2.0f;

    public void AnimateLoseScreen()
    {
        _loseState.gameObject.SetActive(true);

        foreach (Transform child in _loseState.transform)
        {
            child.localScale = new Vector3(0, 0, 0);
            child.DOScale(new Vector3(1, 1, 1), 1.0f);
        }
  
        _loseState.canvasRenderer.SetAlpha(0.0f);
        _loseState.CrossFadeAlpha(targetAlphaValue, fadeOutLenght, false);
    }

    public void AnimateWinScreen()
    {
        _loseState.gameObject.SetActive(true);

        foreach (Transform child in _winState.transform)
        {
            child.localScale = new Vector3(0, 0, 0);
            child.DOScale(new Vector3(1, 1, 1), 1.0f);
        }

        _winState.canvasRenderer.SetAlpha(0.0f);
        _winState.CrossFadeAlpha(targetAlphaValue, fadeOutLenght, false);
    }
}
