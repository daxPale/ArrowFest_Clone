using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField] private Image loseState;
    [SerializeField] private Image winState;
    [SerializeField] private Transform holdAndMove;

    [Range(0, 1)]
    public float targetAlphaValue = 0.5f;
    [Range(0, 5)]
    public float fadeOutLenght = 2.0f;

    public void AnimateLoseScreen()
    {
        loseState.gameObject.SetActive(true);

        foreach (Transform child in loseState.transform)
        {
            child.localScale = new Vector3(0, 0, 0);
            child.DOScale(new Vector3(1, 1, 1), 1.0f);
        }
  
        loseState.canvasRenderer.SetAlpha(0.0f);
        loseState.CrossFadeAlpha(targetAlphaValue, fadeOutLenght, false);
    }

    public void AnimateWinScreen()
    {
        winState.gameObject.SetActive(true);

        foreach (Transform child in winState.transform)
        {
            child.localScale = new Vector3(0, 0, 0);
            child.DOScale(new Vector3(1, 1, 1), 1.0f);
        }

        winState.canvasRenderer.SetAlpha(0.0f);
        winState.CrossFadeAlpha(targetAlphaValue, fadeOutLenght, false);
    }

    public void AnimateFirstTap()
    {
        holdAndMove.DOScaleX(0, 0.4f).SetEase(Ease.InBack);
        //holdAndMove.gameObject.SetActive(false);
    }
}
