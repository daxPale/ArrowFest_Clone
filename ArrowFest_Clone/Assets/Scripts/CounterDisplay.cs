using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CounterDisplay : MonoBehaviour
{
    private Image _backgroundImage;

   public void AnimateCounterDisplay()
    {
        _backgroundImage.transform.DOShakeScale(0.5f, 0.4f);
    }

    private void Awake()
    {
        _backgroundImage = GetComponent<Image>();
    }
}
